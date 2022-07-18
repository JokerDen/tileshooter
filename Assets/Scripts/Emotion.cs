using DG.Tweening;
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
    public float inDuration;

    private Enemy.State lastState = Enemy.State.Default;

    public void Show(Enemy.State state)
    {
        CancelInvoke("Hide");
        
        passive.SetActive(false);
        warned.SetActive(false);
        aggressive.SetActive(false);
        
        gameObject.SetActive(true);
        
        if (state == Enemy.State.Default)
            ShowState(passive);
        if (state == Enemy.State.Warned)
            ShowState(warned);
        if ((state == Enemy.State.Attacking || state == Enemy.State.Alerted) && (lastState == Enemy.State.Default || lastState == Enemy.State.Warned))
            ShowState(aggressive);
        
        lastState = state;
        
        Invoke("Hide", duration);
    }

    private void ShowState(GameObject target)
    {
        target.transform.DOKill();
        target.transform.localScale = Vector3.zero;
        target.SetActive(true);
        target.transform.DOScale(1f, inDuration);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
