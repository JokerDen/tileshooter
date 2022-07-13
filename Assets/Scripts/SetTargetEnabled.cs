using UnityEngine;

public class SetTargetEnabled : MonoBehaviour
{
    public GameObject target;
    
    public Interactible onStartInteraction;
    public Interactible onEndInteraction;

    public Trigger doOnTrigger;

    public bool state;

    private void Start()
    {
        if (onStartInteraction != null)
            onStartInteraction.onStartInteractWith.AddListener(DoDisable);
        if (onEndInteraction != null)
            onEndInteraction.onEndInteractWith.AddListener(DoDisable);

        if (doOnTrigger != null)
            doOnTrigger.onTrigger += Do;
    }

    private void Do()
    {
        if (target != null)
        {
            target.SetActive(state);
        }
    }

    private void DoDisable(Interactible arg0)
    {
        Do();
    }
}
