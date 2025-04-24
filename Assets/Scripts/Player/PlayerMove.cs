using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Références Player")]
    [SerializeField]
    private float moveSpeed;
    public Rigidbody2D rb;
    //public Animator anim;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    //private bool isFacingRight = true;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        //if (anim == null)
        //{
        //    anim = GetComponent<Animator>();
        //}
    }

    private void Start()
    {
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

    //private void Update()
    //{
    //    Animate();
    //} à add après que j'ai fais les anim

    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
        //if (direction.x > 0 && isFacingRight)
        //{
        //    Flip();
        //}
        //else if (direction.x < 0 && !isFacingRight)
        //{
        //    Flip();
        //} pour flip le sprite si symétrique plus simple que beaucoup d'animation
        //à mettre après
    }

    //private void Flip()
    //{
    //    isFacingRight = !isFacingRight;
    //    Vector3 scale = transform.localScale;
    //    scale.x *= -1;
    //    transform.localScale = scale;
    //}

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (rb)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            Debug.LogError("Rigidbody2D is missing on PlayerMove");
        }
    }

    //void Animate()
    //{
    //    if (moveDirection.x != 0 || moveDirection.y != 0)
    //    {
    //        lastMoveDirection = moveDirection;
    //    }

    //    anim.SetFloat("MoveX", moveDirection.x);
    //    anim.SetFloat("MoveY", moveDirection.y);
    //    anim.SetFloat("MoveMagnitude", moveDirection.magnitude);
    //    anim.SetFloat("LastMoveX", lastMoveDirection.x);
    //    anim.SetFloat("LastMoveY", lastMoveDirection.y);
    //} à peu près ça? ou mm c ça quoi c mon topdown
}
