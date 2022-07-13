using UnityEngine;

public class Day1Condition : MonoBehaviour
{
    public StateData gate;

    public int gateRequiredState;

    public StateData dayTime;

    public int setDayState;

    private void Start()
    {
        if (gate.StateIndex == gateRequiredState)
            dayTime.SetIndex(setDayState);
    }
}
