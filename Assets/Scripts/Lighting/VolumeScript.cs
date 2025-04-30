using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;
using Cube;
using Lighting;

public class VolumeScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField] private FilmGrain  grain;
    [SerializeField] private Texture2D lightSp;
    [SerializeField] private Texture2D darkSp;
    [SerializeField] private Volume volume;
    [SerializeField] private GameObject cube;
    [SerializeField] private CubeLight cubeLight;
    [SerializeField] private GameObject player;
    [SerializeField] private LightDetection lightDetection;
    
    void Start()
    {
        volume.profile.TryGet<FilmGrain>(out grain);
        grain.texture.Override(lightSp); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoreGrain()
    {

    }

    void LessGrain()
    {

    }
}
