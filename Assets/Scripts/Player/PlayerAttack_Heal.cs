using UnityEngine;

public class PlayerAttack_Heal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private bool isInFrameToAttack;
    [SerializeField] private bool isInFrameToHeal;
    public Transform cubeTransform;
    private Collider2D attackHealCollider;
    private BoxCollider2D boxCollider2DInteractCube; 
    private void Start()
    {
        isInFrameToAttack = false;
        isInFrameToHeal = false;
        attackHealCollider = GetComponent<Collider2D>();
        boxCollider2DInteractCube = transform.parent.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("CubeHitBox")){
            if(isInFrameToHeal){
                collider2D.transform.parent.transform.gameObject.GetComponent<CubeBehavior>().Heal();
                AudioManager.Instance.PlaySfx(AudioManager.Instance.swordHeal);
            }
        } 
        if(collider2D.CompareTag("Enemy")){
            if(isInFrameToAttack)
            {
                collider2D.gameObject.GetComponent<EnemyBehaviour>().TakeDamage();
                AudioManager.Instance.PlaySfx(AudioManager.Instance.enemyGetHit);
            }
        } 

    }

    public void SetAttackFrameState(bool active){
        isInFrameToAttack = active;
        UpdateColliderState();
        
    }

    public void SetHealFrameState(bool active){
        isInFrameToHeal = active;
        UpdateColliderState();
        
    }

    public void UpdateColliderState()
    {
        attackHealCollider.enabled = isInFrameToHeal || isInFrameToAttack;
        boxCollider2DInteractCube.enabled = !attackHealCollider.enabled;
    }
}
