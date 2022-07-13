using System;
using UnityEngine;

public class ResetStates : MonoBehaviour
{
    public StateData[] states;

    private void Start()
    {
        foreach (var state in states)
        {
            state.Reset();
        }
    }
}
