using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class VolumeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] public FilmGrain  grain;
    [SerializeField] public Texture2D lightSp;
    [SerializeField] public Texture2D darkSp;
    [SerializeField] private Volume volume;
    [SerializeField] private GameObject cube;
    [SerializeField] private GameObject player;
    [SerializeField] private LightDetection lightDetection;
    [SerializeField] private Volume postProcessVolume;
    [SerializeField] private LayerMask affectedLayers;
    
    void Awake()
    {
        volume.profile.TryGet<FilmGrain>(out grain);
        grain.texture.Override(darkSp); 
        volume = GetComponent<Volume>();
        ApplyLayerMask();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ApplyLayerMask()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.gameObject.layer = GetFirstLayerFromMask(affectedLayers);
        }
    }
    
    // Helper method to get the first layer number from a LayerMask
    private int GetFirstLayerFromMask(LayerMask layerMask)
    {
        int layerNumber = 0;
        int value = layerMask.value;
        
        // Find the index of the first set bit
        while (value > 0)
        {
            if ((value & 1) != 0)
                return layerNumber;
                
            value = value >> 1;
            layerNumber++;
        }
        
        return 0; // Default to layer 0 if no layer is selected
    }
    
    private void OnValidate()
    {
        ApplyLayerMask();
    }
}
