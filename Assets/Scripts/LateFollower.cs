using System;
using System.Collections.Generic;
using UnityEngine;

public class LateFollower : MonoBehaviour
{
    [SerializeField] List<Transform> targets = new List<Transform>();

    private void Start()
    {
        // TODO: validate targets (same) and null?
        
    }

    public float smoothing;

    private void LateUpdate()
    {
        if (targets.Count > 0)
        {
            var pos = new Vector3();
            foreach (var target in targets)
                pos += target.position;
            pos /= targets.Count;

            pos = Vector3.Lerp(transform.position, pos, smoothing * Time.deltaTime);
            
            transform.position = pos;
        }
    }

    public void AddTarget(Transform target)
    {
        if (!targets.Contains(target))
            targets.Add(target);
    }

    public void RemoveTarget(Transform target)
    {
        if (targets.Contains(target))
            targets.Remove(target);
    }
}
