using UnityEngine;

public class CubeBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    public void OnGrab()
    {

    }

    public void OffGrab()
    {

    }

    public void Interact()
    {

    }

    private void Move(PlayerMove playerScript)
    {
        rb.linearVelocity = playerScript.moveDirection * playerScript.currentMoveSpeed;
    }
}
