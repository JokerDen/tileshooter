using UnityEngine;

public class AwaitingTrigger : MonoBehaviour
{
    public Trigger trigger;
    
    public Rigidbody awaits;

    private void OnTriggerEnter(Collider other)
    {
        if (awaits == null) return;

        if (other.attachedRigidbody == awaits)
        {
            trigger.Trig();
        }
    }
}
