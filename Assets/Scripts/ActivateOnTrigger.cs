using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{
    public Trigger trigger;

    public GameObject[] targets;

    public bool targetActivity = true;

    private void Start()
    {
        trigger.onTrigger += ActivateTargets;
    }

    private void ActivateTargets()
    {
        // Debug.Log("Activate on Trigger");
        foreach (var target in targets)
        {
            target.SetActive(targetActivity);
        }
    }
}
