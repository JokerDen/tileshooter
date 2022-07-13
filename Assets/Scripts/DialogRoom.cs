using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogRoom : MonoBehaviour
{
    public List<DialogueData> active = new List<DialogueData>();

    public DialogRoomEvent onDialogStarted = new DialogRoomEvent();
    public DialogRoomEvent onDialogEnded = new DialogRoomEvent();

    public void StartDialog(DialogueData dialogue)
    {
        if (active.Contains(dialogue)) return;
        
        active.Add(dialogue);
        onDialogStarted.Invoke(dialogue);
    }

    public void EndDialog(DialogueData dialogue)
    {
        if (!active.Contains(dialogue)) return;

        active.Remove(dialogue);
        onDialogEnded.Invoke(dialogue);
    }
}


public class DialogRoomEvent : UnityEvent<DialogueData>
{
    
}
