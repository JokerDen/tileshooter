using System;
using UnityEngine;

public class Emotion : MonoBehaviour
{
    public EmotionLine[] lineSample;

    public float interval;
    
    private EmotionLine[] lines;

    public GameObject passive;
    public GameObject warned;
    public GameObject aggressive;

    public float duration;

    public void Show(Enemy.State state)
    {
        CancelInvoke("Hide");
        gameObject.SetActive(true);
        
        passive.SetActive(state == Enemy.State.Default);
        warned.SetActive(state == Enemy.State.Warned);
        aggressive.SetActive(state == Enemy.State.Agro);
        
        Invoke("Hide", duration);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
