using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;


public class EnemyBehaviour : MonoBehaviour
{
    [Header("Detection Settings")]
    private Transform cubeTransform;
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private float soundRadius = 4f;
    //[SerializeField] private float chaseSpeed = 2.5f;
    //[SerializeField] private float chaseTime = 4f;
    private LayerMask obstacleLayer;
    private Animator anim;
    [SerializeField] private bool isFound;
    private GameObject cube;
    private bool isAttacking;
    private Light2D light2D;
    private bool isInactive;
    private GameObject player;
    private bool isInSoundRange;
    
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
        anim = GetComponent<Animator>();
        light2D = GetComponent<Light2D>();
        light2D.enabled = false;
        currentState = EnemyState.Search;
        isFound = false;
    }

    private void Start()
    {
        AudioManager.Instance.PlaySfxLoop(AudioManager.Instance.enemyClap);
        cube = GameObject.FindGameObjectWithTag("Cube");
        
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
        float distance = Vector2.Distance(transform.position, cube.transform.position);
        bool nowInSoundRange = distance <= soundRadius;
        if (nowInSoundRange != isInSoundRange)
        {
            isInSoundRange = nowInSoundRange;

            if (isInSoundRange)
                AudioManager.Instance.UnPauseSfxLoop();
            else
                AudioManager.Instance.PauseSfxLoop();
        }

        // Gérer la détection de l’ennemi
        if (!isFound && distance <= detectionRadius)
        {
            isFound = true;
            currentState = EnemyState.Attack;
        }
    }

    private void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;
        Vector3 targetPos = cube.transform.position;
        AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyAttack);
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
        AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyDying);
        gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        anim.SetTrigger("Die");
        AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyGetHit);
    }

    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isAttacking &&  collider is BoxCollider2D)
        {
            if (collider.gameObject.CompareTag("Cube"))
            {
                CubeLight cubeLight = collider.gameObject.transform.GetChild(0).gameObject.GetComponent<CubeLight>();
                if (cubeLight.currentLife != CubeLife.Zero)
                {
                    light2D.enabled = true;
                    light2D.color = collider.gameObject.transform.GetChild(0).GetComponent<Light2D>().color;
                    cubeLight.PreviousLife();
                }
            }
        }
    }
}
