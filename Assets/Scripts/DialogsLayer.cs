using System;
using System.Collections.Generic;

public class DialogsLayer : UILayer
{
    public DialogRoom room;
    
    public DialogUI dialogPrefab;

    private List<DialogUI> active = new List<DialogUI>();

    private void Start()
    {
        room.onDialogStarted.AddListener(ShowDialogue);
        room.onDialogEnded.AddListener(EndDialogue);
    }

    private void EndDialogue(DialogueData arg0)
    {
        foreach (var dialogUI in active)
        {
            if (dialogUI.IsDialogue(arg0))
                dialogUI.ResetAndHide();
        }
    }

    private void ShowDialogue(DialogueData arg0)
    {
        foreach (var dialogUI in active)
        {
            if (dialogUI.IsDialogue(null))
            {
                dialogUI.Show(arg0);
                return;
            }
        }
        
        var item = Instantiate(dialogPrefab, transform);
        active.Add(item);
        item.Show(arg0);
    }
}
