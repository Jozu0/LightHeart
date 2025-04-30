using UnityEditor.VersionControl;
using UnityEngine;


namespace Enemies
{
    public class EnemyBehaviour : MonoBehaviour
    {
        [Header("Detection Settings")]
        public Transform cubeTransform;
        public float detectionRadius = 4f;
        public float chaseSpeed = 2.5f;
        public float chaseTime = 4f;
        public LayerMask obstacleLayer;
        public Rigidbody2D rb;
        public Animator anim;
        public bool isFound;
        public float rangeBat = 2;
        public GameObject cube;

        public enum EnemyState
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

        void Update()
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

        public void Search()
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

        public void Attack()
        {
            //dotween nik ta mere la pute
            //currentState = EnemyState.Cooldown
        }

        public void Cooldown()
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
}
