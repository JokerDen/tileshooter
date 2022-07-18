using System;
using UnityEngine;

public class DistractionSource : MonoBehaviour
{
    public float radius;
    
    private void Start()
    {
        var pos = transform.position;
        var listeners = FindObjectsOfType<DisctractionListener>();
        foreach (var listener in listeners)
        {
            var dist = Vector3.Distance(pos, listener.transform.position);
            if (dist < radius)
                listener.Distract(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}