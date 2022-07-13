using System;
using UnityEngine;

public class TalkInteraction : MonoBehaviour
{
    public DialogueData dialogue;

    private DialogRoom room;
    private Interactible interactible;
    private Interactible interTarget;

    public bool autoStart;
    public Trigger startTrigger;
    public bool oneTime;

    public Trigger onStart;
    public Trigger onEnd;

    public Transform Target => interactible.target;
    
    void Start()
    {
        interactible = GetComponent<Interactible>();
        interactible.hasInteraction = true;
        interactible.onStartInteractWith.AddListener(StartDialogue);

        if (autoStart)
        {
            AutoStart();
        }

        if (startTrigger != null)
            startTrigger.onTrigger += AutoStart;
    }

    private void AutoStart()
    {
        interactible.StartInteract(interactible);
        // StartDialogue(interactible);
    }

    private void StartDialogue(Interactible arg0)
    {
        interTarget = arg0;
        room = null;
        if (dialogue == null)
        {
            End();
            return;
        }
        
        room = GetComponentInParent<DialogRoom>();
        dialogue.Interact(this);
        room.StartDialog(dialogue);
        onStart?.Trig();
    }

    public void End()
    {
        if (room != null)
        {
            room.EndDialog(dialogue);
        }
        // interactible.EndInteraction(interTarget);
        interTarget.EndInteraction(interactible);
        if (oneTime)
        {
            interactible.hasInteraction = false;
            interactible.onStartInteractWith.RemoveListener(StartDialogue);
        }
        onEnd?.Trig();
    }
}
