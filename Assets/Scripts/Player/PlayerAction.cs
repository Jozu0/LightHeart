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
        isAlignToCube = false;
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
            cubeTransform.SetParent(transform);

        }
    }



    public void OffGrab()
    {   
        if(isAlignToCube)
        {   isGrabbing = false;
            playerMove.isCurrentlyGrabbing = isGrabbing;
            cubeTransform.SetParent(null);
        }
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
            cubeTransform.gameObject.GetComponent<CubeBehavior>().isInteractable = true;
        } 

    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {   
        if(collider2D.CompareTag("CubeHitBox")){
            cubeTransform.gameObject.GetComponent<CubeBehavior>().isInteractable = true;
            isAlignToCube = false;
            cubeTransform.SetParent(null);
            isGrabbing = false;
            playerMove.isCurrentlyGrabbing = isGrabbing;
        }
    }
}
