using DG.Tweening;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float doorMovingTime;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interaction()
    {
        if (gameObject.CompareTag("DoorSimple"))
        {
            float width = GetComponent<RectTransform>().rect.width;
            Vector2 newPosition = new Vector2(transform.position.x-width,transform.position.y);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.doorOpen);
            transform.DOMove(newPosition, doorMovingTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                AudioManager.Instance.PlaySfx(AudioManager.Instance.caveNoise);
            });
        }
        if (gameObject.CompareTag("DoorDouble"))
        {
            float width = GetComponent<RectTransform>().rect.width;
            Vector2 newPosition = new Vector2(transform.position.x-(width/2),transform.position.y);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.doorOpen);
            transform.DOMove(newPosition, doorMovingTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                AudioManager.Instance.PlaySfx(AudioManager.Instance.caveNoise);
            });
        }
    }
}
