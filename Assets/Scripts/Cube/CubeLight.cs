using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum CubeColor {White, Red, Blue, Green}
public enum CubeLife {Zero, One, Two, Three} // 0: , 1: ,2: ,3: 6.5f
public class CubeLight : MonoBehaviour
{
    [Header("Current Parameters")]
    public CubeColor currentColor;
    public CubeLife currentLife;
    private CubeColor previousColor;
    private CubeLife previousLife;

    [Header("Transition Parameters")]
    [SerializeField] private float transitionTime;

    [Header("Light Colors")]
    [SerializeField] private Color whiteLight;
    [SerializeField] private Color redLight;
    [SerializeField] private Color blueLight;
    [SerializeField] private Color greenLight;

    [Header("Light Life Parameters")]
    [Header("Zero Life Parameters")]
    [SerializeField] private float zeroLifeIntensity;
    [SerializeField] private float zeroLifeFallOffStrength;
    [Header("One Life Parameters")]
    [SerializeField] private float oneLifeIntensity;
    [SerializeField] private float oneLifeFallOffStrength;
    [Header("Two Life Parameters")]
    [SerializeField] private float twoLifeIntensity;
    [SerializeField] private float twoLifeFallOffStrength;
    [Header("Three Life Parameters")]
    [SerializeField] private float threeLifeIntensity;
    [SerializeField] private float threeLifeFallOffStrength;

    private Light2D light2D;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light2D = GetComponent<Light2D>();
        previousColor = currentColor;
        previousLife = currentLife;
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
            case CubeColor.White:
                DOTween.To(
                    () => light2D.color,
                    x => light2D.color = x,
                    whiteLight,
                    transitionTime
                ).SetEase(Ease.Linear);
                break;
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
        switch(currentLife)
        {
            case CubeLife.Zero: //Intensity : 0.01f
                Sequence LifeToZero = DOTween.Sequence();
                LifeToZero.Join(DOTween.To(
                    () => light2D.intensity,
                    x => light2D.intensity = x,
                    zeroLifeIntensity,
                    transitionTime
                ).SetEase(Ease.Linear));

                LifeToZero.Join(DOTween.To(
                    () => light2D.falloffIntensity,
                    x => light2D.falloffIntensity = x,
                    zeroLifeFallOffStrength,
                    transitionTime
                ).SetEase(Ease.Linear));
                break;

            case CubeLife.One: //Intensity : 1.5, FallOff : 2, FallOffStrength : 0.75f
                Sequence LifeToOne = DOTween.Sequence();
                LifeToOne.Join(DOTween.To(
                    () => light2D.intensity,
                    x => light2D.intensity = x,
                    oneLifeIntensity,
                    transitionTime
                ).SetEase(Ease.Linear));

                LifeToOne.Join(DOTween.To(
                    () => light2D.falloffIntensity,
                    x => light2D.falloffIntensity = x,
                    oneLifeFallOffStrength,
                    transitionTime
                ).SetEase(Ease.Linear));
                break;

            case CubeLife.Two: //Intensity : 4, FallOff : 2, FallOffStrength : 0.6f
                Sequence LifeToTwo = DOTween.Sequence();
                LifeToTwo.Join(DOTween.To(
                    () => light2D.intensity,
                    x => light2D.intensity = x,
                    twoLifeIntensity,
                    transitionTime
                ).SetEase(Ease.Linear));

                LifeToTwo.Join(DOTween.To(
                    () => light2D.falloffIntensity,
                    x => light2D.falloffIntensity = x,
                    twoLifeFallOffStrength,
                    transitionTime
                ).SetEase(Ease.Linear));
                break;
            case CubeLife.Three: //Intensity : 6.5, FallOff : 2, FallOffStrength : 0.5f
                Sequence LifeToThree = DOTween.Sequence();
                LifeToThree.Join(DOTween.To(
                    () => light2D.intensity,
                    x => light2D.intensity = x,
                    threeLifeIntensity,
                    transitionTime
                    ).SetEase(Ease.Linear));

                LifeToThree.Join(DOTween.To(
                    () => light2D.falloffIntensity,
                    x => light2D.falloffIntensity = x,
                    threeLifeFallOffStrength,
                    transitionTime
                ).SetEase(Ease.Linear));
                break; 
        }
        previousLife = currentLife;

    }
}
