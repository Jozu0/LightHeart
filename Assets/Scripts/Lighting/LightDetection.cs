using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float detectionRange;
    [SerializeField] private float verificationDelay;
    [SerializeField] List<GameObject> lightList;
    [SerializeField] public Color colorSum;
    private float lastDetection;
    void Start()
    {
        lastDetection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastDetection)
        {
            lightList = new List<GameObject>();
            lastDetection = Time.time + verificationDelay;
            colorSum = new Color(0,0,0,0);
            Collider2D[] colliders = GetAllTouchedColliders(transform.position, detectionRange);

            foreach (var collider in colliders)
            {
                if(collider.gameObject.CompareTag("CubeHitBox") && !lightList.Contains(collider.gameObject))
                {
                    lightList.Add(collider.gameObject);
                    colorSum += collider.gameObject.transform.parent.transform.GetChild(0).gameObject.GetComponent<Light2D>().color;
                }
            }
        }
    }

    Collider2D[] GetAllTouchedColliders(Vector2 position, float detectionRange)
    {
        return Physics2D.OverlapCircleAll(position, detectionRange);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow; // Couleur du cercle
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    
}
