using System.Collections;
using UnityEngine;

    public class CubeBehavior : MonoBehaviour
    {
        private Rigidbody2D rb;
        private CubeLight CubeLight;
        private float delayNextColor;
        private float timeNextColor;
        public bool isInteractable;
        [SerializeField] private float pushOrGrabFxDelay;
        private float nextFxSound = 0f;
        private Coroutine pushLoopCoroutine;
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
            
            bool isPushing = rb.linearVelocity != Vector2.zero;
            if (isPushing && pushLoopCoroutine == null)
            {
                // On commence la boucle dès qu'on pousse
                pushLoopCoroutine = StartCoroutine(PushLoop());
            }
            else if (!isPushing && pushLoopCoroutine != null)
            {
                StopCoroutine(pushLoopCoroutine);
                pushLoopCoroutine = null;
            }
        }
        
        private IEnumerator PushLoop()
        {
            // On attend 0 frame pour pouvoir jouer tout de suite si tu veux
            yield return null;

            while (true)
            {
                // Ton test avec le timestamp, pour avoir un contrôle précis
                if (Time.time >= nextFxSound)
                {
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.pushCube);
                    nextFxSound = Time.time + pushOrGrabFxDelay;
                }
                // On attend la prochaine frame pour re-vérifier
                yield return null;
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
