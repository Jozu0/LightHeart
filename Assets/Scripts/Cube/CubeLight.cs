using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

    public enum CubeColor {Red, Blue, Green}
    public enum CubeLife {Zero, One, Two, Three} // 0: , 1: ,2: ,3: 6.5f
    public class CubeLight : MonoBehaviour
    {
        [Header("Current Parameters")]
        public CubeColor currentColor;
        public CubeLife currentLife;
        private CubeColor previousColor;
        private CubeLife previousLife;

        [Header("Transition Parameters")]
        [SerializeField] public float transitionTime;
        [SerializeField] public float transitionTimeEffect;

        [Header("Light Colors")]
        [SerializeField] private Color whiteLight;
        [SerializeField] private Color redLight;
        [SerializeField] private Color blueLight;
        [SerializeField] private Color greenLight;

        [Header("Light Life Parameters")]
        [Header("Zero Life Parameters")]
        [SerializeField] private float zeroLifeIntensity;
        [SerializeField] private float zeroLifeFallOffStrength;
        [SerializeField] private float zeroLifeGrainIntensity;
        [Header("One Life Parameters")]
        [SerializeField] private float oneLifeIntensity;
        [SerializeField] private float oneLifeFallOffStrength;
        [SerializeField] private float oneLifeFallOffStrengthEffect;
        [SerializeField] private float oneLifeGrainIntensity;
        [Header("Two Life Parameters")]
        [SerializeField] private float twoLifeIntensity;
        [SerializeField] private float twoLifeFallOffStrength;
        [SerializeField] private float twoLifeFallOffStrengthEffect;
        [SerializeField] private float twoLifeGrainIntensity;
        [Header("Three Life Parameters")]
        [SerializeField] private float threeLifeIntensity;
        [SerializeField] private float threeLifeFallOffStrength;
        [SerializeField] private float threeLifeFallOffStrengthEffect;
        [SerializeField] private float threeLifeGrainIntensity;

        
        [SerializeField] private Light2D light2D;
        [SerializeField] private FilmGrain grain;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            light2D = GetComponent<Light2D>();
            grain = GameObject.FindGameObjectWithTag("Volume").GetComponent<VolumeScript>().grain;
            previousColor = currentColor;
            previousLife = currentLife;
            currentColor = CubeColor.Red;
            light2D.color = redLight;
            currentLife = CubeLife.Three;
       
        }

        // Update is called once per frame
        void Update()
        {
            if(currentColor!=previousColor){
                ColorBehaviour();
            }
            if(currentLife!=previousLife){
                LifeBehaviour();
            }

        }

        private void ColorBehaviour()
        {
            switch(currentColor)
            {
                case CubeColor.Red:
                    DOTween.To(
                        () => light2D.color,
                        x => light2D.color = x,
                        redLight,
                        transitionTime
                    ).SetEase(Ease.Linear);
                    break;
                case CubeColor.Blue:
                    DOTween.To(
                        () => light2D.color,
                        x => light2D.color = x,
                        blueLight,
                        transitionTime
                    ).SetEase(Ease.Linear);
                    break;
                case CubeColor.Green:
                    DOTween.To(
                        () => light2D.color,
                        x => light2D.color = x,
                        greenLight,
                        transitionTime
                    ).SetEase(Ease.Linear);
                    break; 
            }
            previousColor = currentColor;

        }

        private void LifeBehaviour()
        {
            DOTween.Kill(light2D);
            switch(currentLife)
            {
                case CubeLife.Zero: //Intensity : 0.01f
                    Sequence lifeToZero = DOTween.Sequence();
                    lifeToZero.Join(DOTween.To(
                        () => light2D.intensity,
                        x => light2D.intensity = x,
                        zeroLifeIntensity,
                        transitionTime
                    ).SetEase(Ease.Linear));

                    lifeToZero.Join(DOTween.To(
                        () => light2D.falloffIntensity,
                        x => light2D.falloffIntensity = x,
                        zeroLifeFallOffStrength,
                        transitionTime
                    ).SetEase(Ease.Linear));
                    
                    grain.active = true;
                    grain.intensity.value = zeroLifeGrainIntensity;
                    break;
                case CubeLife.One: //Intensity : 1.5, FallOff : 2, FallOffStrength : 0.75f
                    Sequence lifeToOne = DOTween.Sequence();
                    lifeToOne.Join(DOTween.To(
                        () => light2D.intensity,
                        x => light2D.intensity = x,
                        oneLifeIntensity,
                        transitionTime
                    ).SetEase(Ease.Linear));

                    lifeToOne.Join(DOTween.To(
                        () => light2D.falloffIntensity,
                        x => light2D.falloffIntensity = x,
                        oneLifeFallOffStrength,
                        transitionTime
                    ).SetEase(Ease.Linear));
                
                    lifeToOne.OnComplete(() =>
                    {
                        light2D.falloffIntensity = oneLifeFallOffStrength;
                        DOTween.To(
                            () => light2D.falloffIntensity,
                            x => light2D.falloffIntensity = x,
                            oneLifeFallOffStrengthEffect,   
                            transitionTimeEffect
                        ).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetTarget(light2D);
                    });
                    
                    grain.active = true;
                    grain.intensity.value = oneLifeGrainIntensity;
                    break;

                case CubeLife.Two: //Intensity : 4, FallOff : 2, FallOffStrength : 0.6f
                    Sequence lifeToTwo = DOTween.Sequence();
                    lifeToTwo.Join(DOTween.To(
                        () => light2D.intensity,
                        x => light2D.intensity = x,
                        twoLifeIntensity,
                        transitionTime
                    ).SetEase(Ease.Linear));

                    lifeToTwo.Join(DOTween.To(
                        () => light2D.falloffIntensity,
                        x => light2D.falloffIntensity = x,
                        twoLifeFallOffStrength,
                        transitionTime
                    ).SetEase(Ease.Linear));

                    lifeToTwo.OnComplete(() =>
                    {
                        light2D.falloffIntensity = twoLifeFallOffStrength;
                        DOTween.To(
                            () => light2D.falloffIntensity,
                            x => light2D.falloffIntensity = x,
                            twoLifeFallOffStrengthEffect,   
                            transitionTimeEffect
                        ).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetTarget(light2D);
                    });
                    
                    grain.active = true;
                    grain.intensity.value = twoLifeGrainIntensity;
                    break;
                case CubeLife.Three: //Intensity : 6.5, FallOff : 2, FallOffStrength : 0.5f
                    Sequence lifeToThree = DOTween.Sequence();
                    lifeToThree.Join(DOTween.To(
                        () => light2D.intensity,
                        x => light2D.intensity = x,
                        threeLifeIntensity,
                        transitionTime
                    ).SetEase(Ease.Linear));

                    lifeToThree.Join(DOTween.To(
                        () => light2D.falloffIntensity,
                        x => light2D.falloffIntensity = x,
                        threeLifeFallOffStrength,
                        transitionTime
                    ).SetEase(Ease.Linear));

                    lifeToThree.OnComplete(() =>
                    {
                        light2D.falloffIntensity = threeLifeFallOffStrength;
                        DOTween.To(
                            () => light2D.falloffIntensity,
                            x => light2D.falloffIntensity = x,
                            threeLifeFallOffStrengthEffect,   
                            transitionTimeEffect
                        ).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo).SetTarget(light2D);
                    });
                    
                    grain.active = true;
                    grain.intensity.value = threeLifeGrainIntensity;
                    break; 
            }
            previousLife = currentLife;

        }

        public void NextColor()
        {
            currentColor = (CubeColor)(((int)currentColor + 1) % Enum.GetValues(typeof(CubeColor)).Length);
            AudioManager.Instance.PlaySfx(AudioManager.Instance.colorChangeCube);
        }

        public void NextLife()
        {
            if(currentLife < CubeLife.Three){
                currentLife = (CubeLife)((int)currentLife+1);
            }
        }

        public void PreviousLife()
        {
            if(currentLife> CubeLife.Zero)
            {
                currentLife = (CubeLife)((int)currentLife-1);
            }
        }
    }
