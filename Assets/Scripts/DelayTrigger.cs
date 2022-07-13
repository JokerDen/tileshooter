using System;
using UnityEngine;

public class DelayTrigger : MonoBehaviour
{
    public Trigger trigger;

    public float delay;
    
    private void Start()
    {
        Invoke("Trig", delay);
    }

    private void Trig()
    {
        trigger.Trig();
    }
}