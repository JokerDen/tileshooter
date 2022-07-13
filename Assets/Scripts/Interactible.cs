using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{
    public Transform target;

    public float radius;

    private List<Interactible> availableInteractions = new List<Interactible>();

    private static List<Interactible> allCache = new List<Interactible>();

    public bool active;

    public InteractibleEvent onAvailableAdded = new InteractibleEvent();
    public InteractibleEvent onAvailableRemoved = new InteractibleEvent();
    public InteractibleEvent onStartInteractWith = new InteractibleEvent();
    public InteractibleEvent onEndInteractWith = new InteractibleEvent();

    private Interactible interaction;

    public bool hasInteraction;

    public CustomInput input;

    private void Awake()
    {
        if (target == null)
            target = transform;
    }

    private void Start()
    {
        allCache.Add(this);
    }

    private void Update()
    {
        if (!active) return;

        if (input != null && input.IsAction())
        {
            var best = GetClosestAvailable();
            if (best != null)
            {
                // best.Interact
                StartInteract(best);
            }
        }
    }

    public void StartInteract(Interactible interactible)
    {
        interaction = interactible;
        
        Debug.Log($"start {gameObject} to {interactible.gameObject}");

        foreach (var availableInteraction in availableInteractions)
            onAvailableRemoved.Invoke(availableInteraction);
        availableInteractions.Clear();
        onStartInteractWith.Invoke(interactible);
        interactible.onStartInteractWith.Invoke(this);
    }

    public void EndInteraction(Interactible interactible)
    {
        interaction = null;
        onEndInteractWith.Invoke(interactible);
        interactible.onEndInteractWith.Invoke(this);
    }

    public Interactible GetClosestAvailable()
    {
        Interactible best = null;
        float bestDistance = Single.MaxValue;
        Vector3 pos = target.position;
        foreach (var interaction in availableInteractions)
        {
            if (!interaction.HasInteraction()) continue;
            
            var dist = Vector3.Distance(pos, interaction.target.position);
            if (dist < bestDistance)
            {
                best = interaction;
                bestDistance = dist;
            }
        }
        return best;
    }

    private void FixedUpdate()
    {
        // if (!active) return;
        if (interaction != null) return;

        // collect available
        Vector3 pos = target.position;
        foreach (var interactible in allCache)
        {
            if (interactible == this) continue;

            float dist = Vector3.Distance(pos, interactible.target.position);
            if (dist > radius + interactible.radius)
            {
                if (availableInteractions.Contains(interactible))
                {
                    availableInteractions.Remove(interactible);
                    onAvailableRemoved.Invoke(interactible);
                }
            }
            else
            {
                if (!availableInteractions.Contains(interactible))
                {
                    availableInteractions.Add(interactible);
                    onAvailableAdded.Invoke(interactible);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (allCache.Contains(this))
            allCache.Remove(this);
    }

    private void OnDrawGizmosSelected()
    {
        var from = target;
        if (from == null)
            from = transform;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(from.position, radius);
    }

    public bool HasInteraction()
    {
        return hasInteraction;
    }
}

public class InteractibleEvent : UnityEvent<Interactible>
{
    
}
