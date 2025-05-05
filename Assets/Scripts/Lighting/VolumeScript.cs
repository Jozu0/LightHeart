using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

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
    
    void Start()
    {
        volume.profile.TryGet<FilmGrain>(out grain);
        grain.texture.Override(darkSp); 
    }

    // Update is called once per frame
    void Update()
    {

    }

}
