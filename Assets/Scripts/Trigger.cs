using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Action onTrigger;

    public bool oneTime;

    private bool triggered;

    public void Trig()
    {
        if (oneTime && triggered) return;

        triggered = true;
        
        onTrigger.Invoke();
    }
}