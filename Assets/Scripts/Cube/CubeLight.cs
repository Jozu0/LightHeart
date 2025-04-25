using System.Linq.Expressions;
using UnityEngine;

public enum CubeColor {White, Red, Blue, Green}
public enum CubeLife {Zero, One, Two, Three}
public class CubeLight : MonoBehaviour
{
    public CubeColor currentColor;
    public CubeLife currentLife;

    [SerializeField] private Color whiteLight;
    [SerializeField] private Color redLight;
    [SerializeField] private Color blueLight;
    [SerializeField] private Color greenLight;

    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorBehaviour();
        LifeBehaviour();
    }

    private void ColorBehaviour()
    {
        switch(currentColor)
        {
            case CubeColor.White:

                break;
            case CubeColor.Red:

                break;
            case CubeColor.Blue:

                break;
            case CubeColor.Green:
            
                break;
            
        }
    }

    private void LifeBehaviour()
    {

    }
}
