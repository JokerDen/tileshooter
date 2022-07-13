using System.Collections.Generic;

public class InteractionsLayer : UILayer
{
    public Interactible target;
    
    public InteractionUI interactionPrefab;

    public List<InteractionUI> availableInteractions = new List<InteractionUI>();

    public StageController stage;

    private void Awake()
    {
        stage.onTargetChanged.AddListener(UpdateTarget);
    }

    private void UpdateTarget(Interactible arg0)
    {
        if (target != null)
        {
            target.onAvailableAdded.RemoveListener(AddAvailable);
            target.onAvailableRemoved.RemoveListener(RemoveAvailable);
        }

        target = arg0;
        target.onAvailableAdded.AddListener(AddAvailable);
        target.onAvailableRemoved.AddListener(RemoveAvailable);
    }

    private void Update()
    {
        if (target == null) return;
        
        var closest = target.GetClosestAvailable();
        foreach (var interaction in availableInteractions)
        {
            if (closest != null && interaction.IsInteractible(closest))
                interaction.Show();
            else
                interaction.Hide();
        }
    }

    private void RemoveAvailable(Interactible arg0)
    {
        foreach (var interaction in availableInteractions)
        {
            if (interaction.IsInteractible(arg0))
            {
                interaction.Remove();
            }
        }
    }

    private void AddAvailable(Interactible arg0)
    {
        foreach (var interaction in availableInteractions)
        {
            if (interaction.IsInteractible(null))
            {
                interaction.Show(arg0);
                return;
            }
        }

        var newItem = Instantiate(interactionPrefab, transform);
        availableInteractions.Add(newItem);
        newItem.Show(arg0);
    }
}
