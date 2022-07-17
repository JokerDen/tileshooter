using UnityEngine;
using UnityEngine.Events;

public class Collectable : MonoBehaviour
{
    public bool collected;

    public CollectableEvent onCollect = new CollectableEvent();
    
    private void OnTriggerEnter(Collider other)
    {
        if (collected) return;
        var collector = other.GetComponentInParent<Collector>();
        if (collector != null)
        {
            collected = true;
            onCollect.Invoke(collector);
            // collector.Collect(this);
            Destroy(gameObject);
        }
    }
}

public class CollectableEvent : UnityEvent<Collector>
{
    
}
