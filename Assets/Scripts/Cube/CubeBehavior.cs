using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CubeBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    private CubeLight cubeLight;
    private float delayNextColor;
    private float timeNextColor;
    public bool isInteractable;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cubeLight = transform.GetChild(0).GetComponent<CubeLight>();
        delayNextColor = cubeLight.transitionTime+0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent!=null){
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
            cubeLight.NextColor();
            timeNextColor = Time.time + delayNextColor;
        }
    }

    private void Move(PlayerMove playerScript)
    {
        rb.linearVelocity = playerScript.moveDirection * playerScript.currentMoveSpeed;
    }
}
