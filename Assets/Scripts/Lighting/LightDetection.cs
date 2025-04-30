using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Lighting
{
    public class LightDetection : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        [SerializeField] private float detectionRange;
        [SerializeField] private float verificationDelay;
        [SerializeField] private List<GameObject> lightList;
        [SerializeField] public Color colorSum;
        [SerializeField] private Color noColor;
        [SerializeField] private float maxTimeInDark;
        [SerializeField] private float timeSpentInDark; 
        private float m_LastDetection;

        void Start()
        {
            m_LastDetection = 0;
        }

        // Update is called once per frame
        private void Update()
        {
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
                        colorSum += collider2D.gameObject.transform.parent.transform.GetChild(0).gameObject.GetComponent<Light2D>().color;
                    }
                }
            }
            if (gameObject.CompareTag("Player"))
            {
                IsPlayerInLight();
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
            if(colorSum==noColor)
            {
            
            }else{
                timeSpentInDark=0; 
            }
        }



    }
}
