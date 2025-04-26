using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] public bool isGrabbing;
    [SerializeField] private bool isAlignToCube;
    private PlayerMove playerMove;
    public Transform cubeTransform;
    public GameObject cube;
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnGrab()
    {
        if(isAlignToCube)
        {
            isGrabbing = true;
            playerMove.isCurrentlyGrabbing = isGrabbing;
            playerMove.cubeTransform = cubeTransform;
            cube = cubeTransform.gameObject;
            cube.GetComponent<Rigidbody2D>().mass=1f;
            cubeTransform.SetParent(transform);

        }
    }



    public void OffGrab()
    {   
        isGrabbing = false;
        playerMove.isCurrentlyGrabbing = isGrabbing;
        cube.GetComponent<Rigidbody2D>().mass=999999f;
        cube.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        cubeTransform.SetParent(null);
    }

    public void HealCube()
    {

    }


    public void Attack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("CubeHitBox")){
            isAlignToCube = true;
            cubeTransform = collider2D.transform.parent.transform;
        } 

    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {   
        if(collider2D.CompareTag("CubeHitBox")){
            isAlignToCube = false;
            cubeTransform.SetParent(null);
            cube.GetComponent<Rigidbody2D>().mass=999999f;
            cube.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
