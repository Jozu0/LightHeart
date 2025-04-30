using Cube;
using Enemies;
using UnityEngine;

public class PlayerAttack_Heal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    [SerializeField] private bool isInFrameToAttack;
    [SerializeField] private bool isInFrameToHeal;
    public Transform cubeTransform;
    private Collider2D attackHealCollider;
    void Start()
    {
        isInFrameToAttack = false;
        isInFrameToHeal = false;
        attackHealCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("CubeHitBox")){
            if(isInFrameToHeal){
                collider2D.transform.parent.transform.gameObject.GetComponent<CubeBehavior>().Heal();
            }
        } 
        if(collider2D.CompareTag("Enemy")){
            if(isInFrameToAttack)
            {
                collider2D.gameObject.GetComponent<EnemyBehaviour>().TakeDamage();
            }
        } 

    }

    public void InFrameToAttack(){
        isInFrameToAttack = !isInFrameToAttack;
        attackHealCollider.enabled = !attackHealCollider.enabled;
    }

    public void InFrameToHeal(){
        isInFrameToHeal = !isInFrameToHeal;
        attackHealCollider.enabled = !attackHealCollider.enabled;
    }
}
