using UnityEngine;

public class SleepInteraction : MonoBehaviour
{
    public StateData dayTime;
    
    void Start() 
    {
        var interactible = GetComponent<Interactible>();

        if (dayTime.StateIndex != 2)
            gameObject.SetActive(false);
    }
}
