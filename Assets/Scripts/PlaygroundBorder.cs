using DG.Tweening;
using UnityEngine;

public class PlaygroundBorder : MonoBehaviour
{
    public StateData state;

    public Vector3 localAngle;
    public float duration;
    public Ease ease;

    public GameObject[] disable;

    public TalkInteraction finishToState;
    
    public TalkInteraction changeTalk;
    public DialogueData toTalk;

    public void Start()
    {
        finishToState.onEnd.onTrigger += ChangeState;
        
        state.onChanged.AddListener(HandleChange);

        if (state.StateIndex == 1)
        {
            transform.localEulerAngles = localAngle;

            Apply();
        }
    }

    private void ChangeState()
    {
        state.SetIndex(1);
    }

    private void Apply()
    {
        foreach (var target in disable)
            target.SetActive(false);
            
        changeTalk.dialogue = toTalk;
    }

    private void HandleChange()
    {
        transform.DOLocalRotate(localAngle, duration).SetEase(ease);
        Apply();
    }
}
