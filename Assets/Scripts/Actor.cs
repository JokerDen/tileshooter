using System;
using UnityEngine;
using UnityEngine.AI;

public class Actor : MonoBehaviour
{
    public CharacterData character;

    public NavMeshAgent agent;


    private Transform targetPos;
    private Action targetCallback;

    public void MoveTo(Transform targetPos, Action action)
    {
        this.targetPos = targetPos;
        targetCallback = action;
    }

    private void FixedUpdate()
    {
        var position = targetPos.position;
        if (Vector3.Distance(transform.position, position) <= agent.stoppingDistance + agent.radius)
        {
            targetCallback.Invoke();
            targetCallback = null;
            return;
        }
        
        agent.SetDestination(position);
    }
}
