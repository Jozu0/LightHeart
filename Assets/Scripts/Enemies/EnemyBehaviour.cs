using UnityEngine;

    public class EnemyBehaviour : MonoBehaviour
    {
        [Header("Detection Settings")]
        private Transform cubeTransform;
        [SerializeField] private float detectionRadius = 4f;
        [SerializeField] private float chaseSpeed = 2.5f;
        [SerializeField] private float chaseTime = 4f;
        private LayerMask obstacleLayer;
        private Rigidbody2D rb;
        private Animator anim;
        [SerializeField] private bool isFound;
        [SerializeField] private float rangeBat = 2;
        private GameObject cube;

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
            if (isFound == false)
            {
                Collider2D cubeCollider = Physics2D.OverlapCircle(transform.position, rangeBat);
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
            //dotween nik ta mere la pute
            //currentState = EnemyState.Cooldown
        }

        private void Cooldown()
        {

        }

        public void Died()
        {
            gameObject.SetActive(false);
        }

        public void TakeDamage()
        {

        }

    }
