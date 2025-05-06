using UnityEngine;

public class PressurePlateDetect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Color pressurePlateColor;
    [SerializeField] private bool isPressed = false;
    [SerializeField] private Sprite normalPlate;
    [SerializeField] private Sprite pressedPlate;
    [SerializeField] private GameObject gameObjectToInteractWith; 
    
    private InteractionScript interactionScript;
    private SpriteRenderer platesSpriteRenderer;
    private LightDetection lightDetection;

    void Start()
    {
        platesSpriteRenderer = GetComponent<SpriteRenderer>();
        platesSpriteRenderer.sprite = normalPlate;
        interactionScript =  gameObjectToInteractWith.GetComponent<InteractionScript>();
        lightDetection = GetComponent<LightDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isPressed == false && (other.gameObject.CompareTag("Cube") || other.gameObject.CompareTag("CubeHitBox")))
        { 
            if (IsColorApproximately(pressurePlateColor, lightDetection.colorSum))
            {
                isPressed = true;
                lightDetection.isPushedLightDetect = true;
                platesSpriteRenderer.sprite = pressedPlate;
                interactionScript.Interaction();
                AudioManager.Instance.PlaySfx(AudioManager.Instance.pressurePlateActivate);
            }
            
        }
    }
    
    
    private bool IsColorApproximately(Color a, Color b, float tolerance = 0.05f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
