using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField] private float moveSpeed;
    [SerializeField] public float currentMoveSpeed;
    [SerializeField] private float grabbingMoveSpeed;
    [SerializeField] private Collider2D playerCubeCollider;
    [SerializeField] private Collider2D playerActionCollider;
    private Rigidbody2D rb;
    private PlayerAction playerAction;
    private Animator anim;
    public Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private bool isFacingRight = true;
    private bool isWalking = false;
    public bool isCurrentlyGrabbing;
    public Transform cubeTransform;
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
        PlayerInput playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            if (InputManager.Instance != null)
            {
                InputManager.Instance.SetPlayerInput(playerInput);
            }
            else
            {
                Debug.LogError("InputManager is not in the scene");
            }
        }
        else
        {
            Debug.LogError("Missing PlayerInput on GameObject");
        }
    }

    private void Update()
    {
        Animate();
        bool shouldWalk = rb.linearVelocity.sqrMagnitude > 0.01f;

        if (shouldWalk && !isWalking)
        {
            // on démarre la marche
            AudioManager.Instance.PlayWalk(AudioManager.Instance.walk);
            isWalking = true;
        }
        else if (!shouldWalk && isWalking)
        {
            // on arrête la marche
            AudioManager.Instance.StopWalking();
            Debug.Log("là il marche PAS hein");
            isWalking = false;
        }
    }

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
        if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        //pour flip le sprite si sym�trique plus simple que beaucoup d'animation � mettre apr�s
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        // Vector3 scale = transform.localScale;
        // scale.x *= -1;
        // transform.localScale = scale;
        GetComponent<SpriteRenderer>().flipX = !isFacingRight;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (rb)
        {
            rb.linearVelocity = moveDirection * currentMoveSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on PlayerMove");
        }
    }

    void Animate()
    {
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }
        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.y);
        anim.SetFloat("MoveMagnitude", moveDirection.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
        if(isCurrentlyGrabbing){
            currentMoveSpeed = grabbingMoveSpeed;
            if (moveDirection.y != 0 && moveDirection.x == 0)
            {
                HandlePushPull(transform.position.y, cubeTransform.position.y, moveDirection.y);
            }
            else if (moveDirection.x != 0 && moveDirection.y == 0)
            {
                HandlePushPull(transform.position.x, cubeTransform.position.x, moveDirection.x);
            }
        }else{
            currentMoveSpeed = moveSpeed;
            anim.SetBool("Push", false);
            anim.SetBool("Pull", false);
        }


        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(lastMoveDirection.x) > Mathf.Abs(lastMoveDirection.y))
        {
            direction = new Vector2(Mathf.Sign(lastMoveDirection.x)*0.8f, 0);
        }
        else
        {
            direction = new Vector2(0, Mathf.Sign(lastMoveDirection.y)*0.5f);
        }

        if (anim.GetBool("Pull"))
        {
            direction = -direction; // Inverse la direction si Pull
        }
        playerActionCollider.offset = direction;
        playerCubeCollider.offset = direction;
    }

    void HandlePushPull(float playerPos, float cubePos, float moveAxis)
    {
        bool isPush = (playerPos < cubePos && moveAxis > 0) || (playerPos > cubePos && moveAxis < 0);
        anim.SetBool("Push", isPush);
        anim.SetBool("Pull", !isPush);
    }



}
