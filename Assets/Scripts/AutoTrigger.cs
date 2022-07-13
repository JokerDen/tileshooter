using UnityEngine;

public class AutoTrigger : MonoBehaviour
{
    public Trigger trigger;

    private void Start()
    {
        trigger.Trig();
    }
}
