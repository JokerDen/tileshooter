using DG.Tweening;
using UnityEngine;

public class FlickLight : MonoBehaviour
{
    private Light light;

    public float transitionTime;
    
    private float startIntensity;
    
    public float minTime;
    public float maxTime;

    private bool currentState;
    
    private void Start()
    {
        light = GetComponent<Light>();
        startIntensity = light.intensity;

        Flick();
    }

    private void Flick()
    {
        currentState = !currentState;

        light.DOIntensity(currentState ? startIntensity : 0f, transitionTime).OnComplete(Flick)
            .SetDelay(Random.Range(minTime, maxTime));
    }
}
