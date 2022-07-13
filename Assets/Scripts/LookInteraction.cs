using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class LookInteraction : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> lookQueue = new List<Transform>();

    private Transform lookTarget;

    public UnityEvent onLook;

    private void Start()
    {
        var interactible = GetComponent<Interactible>();
        interactible.onAvailableAdded.AddListener(AddLook);
        interactible.onAvailableRemoved.AddListener(RemoveLook);
    }

    private void Update()
    {
        if (lookQueue.Count > 0)
        {
            var target = lookQueue[0];
            if (lookTarget != target)
            {
                lookTarget = target;
                onLook.Invoke();
            }
        }
        else
        {
            lookTarget = null;
        }
        
        var hasLookTarget = lookTarget != null;
        agent.updateRotation = !hasLookTarget;
        if (hasLookTarget)
        {
            var thisPos = transform.position;
            Vector3 direction = lookTarget.position - thisPos;
            // direction.y = thisPos.y;
            direction.y = 0f;
            direction = direction.normalized;
            
            var angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            
            var maxAngle = agent.angularSpeed * Time.fixedDeltaTime;
            var localAngles = transform.localEulerAngles;
            localAngles.y += Mathf.Clamp(angle, -maxAngle, maxAngle);
            transform.localEulerAngles = localAngles;

            // var lookAngle = Vector3.Angle(transform.forward, direction);

            /*var lookDiff = lookTarget.position - lookCamPos.position;
            var h = lookDiff.y;
            lookDiff.y = 0f;
            var l = lookDiff.magnitude;

            float lookA = Mathf.Atan(h / l) * Mathf.Rad2Deg;

            var locAngle = lookCamPos.localEulerAngles;
            locAngle.x = -lookA;
            lookCamPos.localEulerAngles = locAngle;*/

            // lookCamPos.LookAt(lookTarget);
        }
        else
        {
            // lookCamPos.localRotation = Quaternion.identity;
        }
    }

    private void RemoveLook(Interactible arg0)
    {
        lookQueue.Remove(arg0.target);
    }

    private void AddLook(Interactible arg0)
    {
        lookQueue.Add(arg0.target);
    }
}
