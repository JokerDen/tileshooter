using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private List<Transform> chain = new List<Transform>();

    public bool main;

    public void AddChain(Transform target)
    {
        if (chain.Contains(target)) return;

        chain.Add(target);
    }

    public Vector3 GetFollowPos(Transform target)
    {
        if (chain.Contains(target))
        {
            var idx = chain.IndexOf(target);
            if (idx > 0)
                return chain[idx - 1].position;
        }

        return transform.position;
    }
}