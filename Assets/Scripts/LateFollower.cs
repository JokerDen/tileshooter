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

    private void LateUpdate()
    {
        if (targets.Count > 0)
        {
            var pos = new Vector3();
            foreach (var target in targets)
                pos += target.position;
            pos /= targets.Count;
            
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
