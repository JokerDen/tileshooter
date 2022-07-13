using UnityEngine;

public class InteractibleEnvTransit : MonoBehaviour
{
    private Interactible interactible;

    public string toStage;
    public int stateIndex;

    public Trigger trigger;
    
    void Start()
    {
        interactible = GetComponent<Interactible>();
        if (interactible != null)
            interactible.onStartInteractWith.AddListener(Transit);

        if (trigger != null)
            trigger.onTrigger += Transit;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactible = other.GetComponent<Interactible>();
        if (interactible != null)
            Transit(interactible);
    }

    private void Transit(Interactible arg0)
    {
        Transit();
    }

    private void Transit()
    {
        var stages = FindObjectsOfType<StageController>();
        foreach (var stage in stages)
        {
            if (stage.enabled)
                stage.Load(toStage, stateIndex);
        }
    }
}
