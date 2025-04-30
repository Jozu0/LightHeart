using UnityEngine;

namespace Cube
{
    public class CubeBehavior : MonoBehaviour
    {
        private Rigidbody2D rb;
        private CubeLight CubeLight;
        private float delayNextColor;
        private float timeNextColor;
        public bool isInteractable;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            CubeLight = transform.GetChild(0).GetComponent<CubeLight>();
            delayNextColor = CubeLight.transitionTime+0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            if(transform.parent){
                if(transform.parent.CompareTag("Player"))
                {
                    Move(transform.parent.gameObject.GetComponent<PlayerMove>());
                }
            }else{
                rb.linearVelocity = Vector2.zero;
            }
        
        }

        public void Interact()
        {
            if(isInteractable && Time.time >= timeNextColor)
            {
                CubeLight.NextColor();
                timeNextColor = Time.time + delayNextColor;
            }
        }

        private void Move(PlayerMove playerScript)
        {
            rb.linearVelocity = playerScript.moveDirection * playerScript.currentMoveSpeed;
        }

        public void Heal()
        {
            CubeLight.NextLife();
        }
    }
}
