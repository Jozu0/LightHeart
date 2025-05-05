using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public float smoothSpeed = 5f; // Valeur plus �lev�e car on l'utilise avec deltaTime
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Boundary Settings")]
    public bool useBoundaries = true;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;

    [Header("Advanced Settings")]
    public float snapDistance = 0.05f; // Distance en dessous de laquelle la cam�ra se positionne exactement sur la cible

    [SerializeField] private GameObject dyingScreen;
    private void LateUpdate()
    {
        if (!target)
            return;

        // Calcule la position d�sir�e
        Vector3 desiredPosition = target.position + offset;

        // Calcule la distance � la position d�sir�e
        float distance = Vector3.Distance(transform.position, desiredPosition);

        // Si on est tr�s proche, snap directement � la position pour �viter les micro-tremblements
        if (distance < snapDistance)
        {
            transform.position = desiredPosition;
            return;
        }

        // Calcule le mouvement en tenant compte du deltaTime
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Applique les limites si activ�es
        if (useBoundaries)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minX, maxX);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minY, maxY);
        }

        // Assigne la position finale
        transform.position = smoothedPosition;
        dyingScreen.transform.position =  transform.position;

    }

    // M�thode pour d�finir les limites de la cam�ra en fonction des limites de la carte
    public void SetBoundaries(float minX, float maxX, float minY, float maxY)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }

}
