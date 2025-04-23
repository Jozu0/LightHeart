using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightColorChange : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Light2D light2D;
    private GameObject cube;
    
    void Start()
    {
        light2D = GetComponent<Light2D>();
        cube = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        light2D.color = cube.GetComponent<SpriteRenderer>().color;

    }

}
