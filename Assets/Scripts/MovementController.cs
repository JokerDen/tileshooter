using System;
using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour
{
    public CustomInput input;

    public NavMeshAgent agent;

    public Animator anim;
    public int legs;
    private int leg;
    private bool wasWalking;

    public float runRatio;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private bool skipStep = true;

    public int pitchPerBeat;

    public Interactible busy;

    private bool isBusy;

    public float boostDuration;
    public float originSpeed;
    public ParticleSystem boostEffect;

    private void Start()
    {
        originSpeed = agent.speed;

        if (busy != null)
        {
            busy.onStartInteractWith.AddListener(SetBusy);
            busy.onEndInteractWith.AddListener(SetFree);
        }
    }

    private void SetFree(Interactible arg0)
    {
        isBusy = false;
    }

    private void SetBusy(Interactible arg0)
    {
        isBusy = true;
    }

    private void Update()
    {
        var em = boostEffect.emission;
        em.enabled = boostDuration > 0f;
    }

    private void FixedUpdate()
    {
        if (boostDuration > 0f)
        {
            boostDuration -= Time.deltaTime;
            if (boostDuration <= 0f)
                agent.speed = originSpeed;
        }
        
        if (input != null)
        {
            var vel = input.movement;
            if (isBusy)
                vel = Vector3.zero;
            // if (vel.magnitude > 0f)
                // vel.Normalize();
            agent.velocity = vel * agent.speed;
        }
        
        var walking = agent.velocity.magnitude > 0f;
        if (!wasWalking && walking)
        {
            skipStep = true;
        }
        
        anim.SetBool("IsWalking", walking);
        anim.SetBool("IsRun", agent.velocity.magnitude >= agent.speed * runRatio);
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
    public void Step()
    {
        if (skipStep)
        {
            skipStep = false;
            return;
        }
        
        // audioSource.PlayOneShot(audioClip, 0.5f + Random.value * .5f);
        // audioSource.pitch = 0.5f + Random.value * 0.5f;
        audioSource.PlayOneShot(audioClip);
    }

    public void AddBoost(float boostedSpeed, float duration)
    {
        agent.speed = boostedSpeed;
        boostDuration = duration;
    }
}
