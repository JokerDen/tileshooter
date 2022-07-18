using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController character;

    public float speed;
    public float runSpeed;

    public Animator anim;
    public int legs;
    private int leg;
    private bool wasWalking;
    private bool skipStep = true;

    private bool walking;
    public bool running;
    public bool manualUpdate;

    private Vector3 destination;

    private void Awake()
    {
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        if (!wasWalking && walking)
        {
            skipStep = true;
        }
        
        anim.SetBool("Walk", walking);
        anim.SetBool("Run", running);
        if (wasWalking && !walking)
        {
            leg++;
            if (leg >= legs)
                leg = 0;
            anim.SetInteger("Leg", leg);
        }

        wasWalking = walking;
    }

    // by animator
    /*public void Step()
    {
        if (skipStep)
        {
            skipStep = false;
            return;
        }
        
        // audioSource.PlayOneShot(audioClip, 0.5f + Random.value * .5f);
        // audioSource.pitch = 0.5f + Random.value * 0.5f;
        audioSource.PlayOneShot(audioClip);
    }*/
    
    private void Update()
    {
        if (!manualUpdate)
            UpdateMove();
    }

    public void UpdateMove()
    {
        var move = Physics.gravity * Time.deltaTime;

        var thisSpeed = running ? runSpeed : speed;
        thisSpeed *= Time.deltaTime;

        Vector3 diff = destination - transform.position;
        walking = diff.magnitude > character.skinWidth;
        diff = Vector3.ClampMagnitude(diff, Mathf.Min(diff.magnitude, thisSpeed));

        move += diff;

        character.Move(move);
    }

    public void MoveToPoint(Vector3 point)
    {
        destination = point;
    }
}