using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

    public class LightDetection : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private float detectionRange;
        [SerializeField] private float verificationDelay;
        [SerializeField] private List<GameObject> lightList;
        [SerializeField] public Color colorSum;
        [SerializeField] private Color noColor;
        [SerializeField] private float maxTimeInDark;
        [SerializeField] private float timeGetBackNormal; 
        private float m_LastDetection;
        private VolumeScript volumeScript;
        private FilmGrain grain;
        private SpriteRenderer dyingScreenTexture; 
        [SerializeField] private Color dyingScreenColorDesactivate;
        [SerializeField] private Color dyingScreenColorActivate;
        private bool isDoingTransition;
        private bool playerIsInTheDark;
        private Sequence currentSequence;
        public bool isPushedLightDetect;
        [SerializeField] private Scene_Switch sceneSwitch;
        void Start()
        {
            m_LastDetection = 0;
            sceneSwitch = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<Scene_Switch>();
            if (gameObject.CompareTag("Player"))
            {
                volumeScript = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeScript>();
                grain = volumeScript.grain;
                dyingScreenTexture = GameObject.FindGameObjectWithTag("DyingScreen").GetComponent<SpriteRenderer>();
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (gameObject.CompareTag("PressurePlate") && isPushedLightDetect == true) return;
            if (Time.time >= m_LastDetection)
            {
                lightList = new List<GameObject>();
                m_LastDetection = Time.time + verificationDelay;
                colorSum = new Color(0,0,0,0);
                Collider2D[] colliders = GetAllTouchedColliders(transform.position, detectionRange);

                foreach (var collider2D in colliders)
                {
                    if(collider2D.gameObject.CompareTag("CubeHitBox") && !lightList.Contains(collider2D.gameObject))
                    {
                        lightList.Add(collider2D.gameObject);
                        colorSum += collider2D.gameObject.transform.parent.transform.GetChild(0).gameObject
                            .GetComponent<Light2D>().color;
                    }
                }
                if (gameObject.CompareTag("Player"))
                {
                    IsPlayerInLight();
                }
            }
            
            
        }

        Collider2D[] GetAllTouchedColliders(Vector2 position, float range)
        {
            return Physics2D.OverlapCircleAll(position, range);
        }


        // void OnDrawGizmosSelected()
        // {
        //     Gizmos.color = Color.yellow; // Couleur du cercle
        //     Gizmos.DrawWireSphere(transform.position, detectionRange);
        // }
    
        private void IsPlayerInLight()
        {
            if (IsColorApproximately(colorSum, noColor) && !playerIsInTheDark)
            {
                playerIsInTheDark = true;

                currentSequence?.Kill();

                grain.texture.value = volumeScript.darkSp;
                currentSequence = DOTween.Sequence();

                currentSequence.Join(DOTween.To(
                    () => grain.intensity.value,
                    x => grain.intensity.value = x,
                    1,
                    maxTimeInDark
                ).SetEase(Ease.Linear));

                currentSequence.Join(DOTween.To(
                    () => dyingScreenTexture.color,
                    x => dyingScreenTexture.color = x,
                    dyingScreenColorActivate,
                    maxTimeInDark
                ).SetEase(Ease.Linear));

                currentSequence.OnComplete(() =>
                {
                    AudioManager.Instance.PauseSfxLoop();
                    AudioManager.Instance.PlaySfx(AudioManager.Instance.death);
                    sceneSwitch.sceneSwitch("MainMenuScene");
                });
            }
            else if (!IsColorApproximately(colorSum, noColor) && playerIsInTheDark)
            {
                playerIsInTheDark = false;

                currentSequence?.Kill();

                currentSequence = DOTween.Sequence();
                currentSequence.Join(DOTween.To(
                    () => grain.intensity.value,
                    x => grain.intensity.value = x,
                    0.15f,
                    timeGetBackNormal
                ).SetEase(Ease.Linear));
    
                currentSequence.Join(DOTween.To(
                    () => dyingScreenTexture.color,
                    x => dyingScreenTexture.color = x,
                    dyingScreenColorDesactivate,
                    timeGetBackNormal
                ).SetEase(Ease.Linear));
            }


        }
        
        private bool IsColorApproximately(Color a, Color b, float tolerance = 0.05f)
        {
            return Mathf.Abs(a.r - b.r) < tolerance &&
                   Mathf.Abs(a.g - b.g) < tolerance &&
                   Mathf.Abs(a.b - b.b) < tolerance;
        }
    }
