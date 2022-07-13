using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(order = 1, menuName = "State Data", fileName = "State")]
public class StateData : ScriptableObject
{
    public int StateIndex
    {
        get
        {
            if (!inited)
            {
                inited = true;
                stateIndex = startIndex;
            }
            
            return stateIndex;
        }
    }

    public int startIndex;

    private int stateIndex;
    private bool inited;
    
    public UnityEvent onChanged = new UnityEvent();

    public void SetIndex(int index)
    {
        Debug.Log($"try set {this} to {index} with current {StateIndex} {inited}");
        
        if (StateIndex == index) return;
        
        stateIndex = index;
        onChanged.Invoke();
    }

    public void Reset()
    {
        stateIndex = startIndex;
    }
}