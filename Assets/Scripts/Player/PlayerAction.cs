using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] public bool isGrabbing;
    [SerializeField] private bool isAlignToCube;
    [SerializeField] private float attackDelay;
    [SerializeField] private float healDelay;
    private float lastActionTime;
    private PlayerMove playerMove;
    public Transform cubeTransform;
    public GameObject cube;
    private Animator anim;
    private PlayerAttack_Heal playerAttack_Heal;
    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        anim = GetComponent<Animator>();
        playerAttack_Heal = transform.GetChild(0).GetComponent<PlayerAttack_Heal>();
        isAlignToCube = false;
        lastActionTime = 0;
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
        if(Time.time >= lastActionTime)
        {
            lastActionTime = Time.time + healDelay;
            anim.SetTrigger("Heal");
        }
    }


    public void Attack()
    {
        if(Time.time >= lastActionTime)
        {
            lastActionTime = Time.time + attackDelay;
            anim.SetTrigger("Attack");

        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("CubeHitBox")){
            cubeTransform = collider2D.transform.parent.transform;
            isAlignToCube = true;
            cubeTransform.gameObject.GetComponent<CubeBehavior>().isInteractable = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collider2D)
    {   
        if(collider2D.CompareTag("CubeHitBox")){
            cubeTransform.gameObject.GetComponent<CubeBehavior>().isInteractable = false;
            isAlignToCube = false;
            cubeTransform.SetParent(null);
            isGrabbing = false;
            playerMove.isCurrentlyGrabbing = isGrabbing;
        }

    }

    public void CallInFrameToAttack(){
        playerAttack_Heal.InFrameToAttack();
    }

    public void CallInFrameToHeal(){
        playerAttack_Heal.InFrameToHeal();
    }

}
