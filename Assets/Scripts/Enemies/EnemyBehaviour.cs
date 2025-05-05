using UnityEngine;
using DG.Tweening;


public class EnemyBehaviour : MonoBehaviour
{
    [Header("Detection Settings")]
    private Transform cubeTransform;
    [SerializeField] private float detectionRadius = 4f;
    //[SerializeField] private float chaseSpeed = 2.5f;
    //[SerializeField] private float chaseTime = 4f;
    private LayerMask obstacleLayer;
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private bool isFound;
    private GameObject cube;
    private bool isAttacking = false;
    


    [Header("Cooldown Settings")]
    [SerializeField] private float attackCooldownDuration = 5f;
    private float cooldownTimer;

    private enum EnemyState
    {
        Search,
        Attack,
        Cooldown
    }

    private EnemyState currentState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentState = EnemyState.Search;
        isFound = false;
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Search:
                Search();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Cooldown:
                Cooldown();
                break;
        }
    }

    private void Search()
    {
        Collider2D cubeCollider = Physics2D.OverlapCircle(transform.position, detectionRadius);

        if (isFound == false)
        {
            if (cubeCollider.gameObject.CompareTag("Cube"))
            {
                isFound = true;
                cube = cubeCollider.gameObject;
                currentState = EnemyState.Attack;
            }
        }
    }

    private void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        Vector3 targetPos = cube.transform.position;

        transform.DOMove(targetPos, 0.5f)
        .SetLoops(2, LoopType.Yoyo)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            cooldownTimer = attackCooldownDuration;
            currentState = EnemyState.Cooldown;
            isAttacking = false;
        });
    }

    private void Cooldown()
    {
        isFound = false;
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            currentState = EnemyState.Search;
        }
    }

    public void Died()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        anim.SetTrigger("Die");
    }

    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
