using UnityEngine;
using UnityEngine.AI;

public class InteractionInterFollower : MonoBehaviour
{
    public NavMeshAgent agent;
    public Interactible interaction;

    public float followDistance = 1f;
    public FollowTarget followTarget;

    private void Start()
    {
        interaction.onAvailableAdded.AddListener(StartFollow);
    }

    private void StartFollow(Interactible arg0)
    {
        if (followTarget != null) return;

        followTarget = arg0.GetComponent<FollowTarget>();
        if (followTarget != null || !followTarget.main)
        {
            followTarget.AddChain(transform);
        }
    }

    private void FixedUpdate()
    {
        if (followTarget == null) return;

        var pos = transform.position;
        var followPos = followTarget.GetFollowPos(transform);
        var dist = Vector3.Distance(pos, followPos);
        if (dist > followDistance)
        {
            agent.destination = followPos;
        }
        else
        {
            agent.destination = pos;
        }
    }
}
