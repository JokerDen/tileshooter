using UnityEngine;

public class ChangeState : MonoBehaviour
{
    public Trigger trigger;
    public StateData state;
    public int index;
    
    void Start()
    {
        trigger.onTrigger += Change;
    }

    private void Change()
    {
        state.SetIndex(index);
    }
}
