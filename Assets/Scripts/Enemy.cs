using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public Weapon weapon;
    
    public enum State
    {
        Default = 0,
        Warned = 50,
        Attacking = 100,
        Alerted = 150,
    }

    public State state;

    public CastForward forwardCast;
    public CastForward rightCast;
    public CastForward leftCast;

    private Damageable target;

    public float preShotDelay;
    public float postShotDelay;
    public float alertLookDuration;
    public int alertLookNum;
    private int alertCurrentLookNum;
    public float distractedLookDuration;
    public float passiveDelay;

    public int[] hostileTeams;

    public Emotion emotion;

    public Route route;
    private int routeIndex;
    private float waitDuration;

    private Coroutine ai;

    public DisctractionListener distraction;

    public CharacterMovement movement;

    private void Start()
    {
        distraction.onDistraction.AddListener(HandleDistraction);
        StartAI();
    }

    private void HandleDistraction(DistractionSource arg0)
    {
        if (state != State.Attacking)
        {
            var dir = arg0.transform.position - transform.position;
            LookToDirection(dir);
            
            if (state != State.Alerted)
                SetState(State.Warned);
        }
    }

    private void LookToDirection(Vector3 direction)
    {
        var angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);
        angle = Mathf.Round(angle / 45f) * 45f;
        var angles = transform.localEulerAngles;
        angles.y = angle;
        transform.localEulerAngles = angles;
    }

    private void StartAI()
    {
        if (ai != null)
            StopCoroutine(ai);
        ai = StartCoroutine(AI());
    }

    private void SetState(State state)
    {
        if (this.state == state) return;
        movement.MoveToPoint(transform.position);
        this.state = state;
        emotion.Show(state);
        StartAI();
    }

    private IEnumerator PassiveAI()
    {
        RoutePoint routePoint = null;
        waitDuration = passiveDelay;
        while (true)
        {
            target = forwardCast.GetTarget(hostileTeams);

            if (target != null)
            {
                SetState(State.Attacking);
                yield break;
            }

            float time = Time.time;
            yield return new WaitForFixedUpdate();
            var dt = Time.time - time;

            if (routePoint != null)
            {
                var routePos = routePoint.transform.position;
                var diff = routePos - transform.position;
                
                if (diff.magnitude < 0.05)
                {
                    waitDuration = routePoint.waitDuration;
                    transform.rotation = routePoint.transform.rotation;
                    routePoint = null;
                    movement.MoveToPoint(transform.position);
                }
                else
                {
                    LookToDirection(diff);
                    movement.MoveToPoint(routePos);
                    continue;
                }
            }

            if (route != null)
            {
                waitDuration -= dt;

                if (waitDuration <= 0f)
                {
                    routeIndex = route.GetNextIndex(routeIndex);
                    routePoint = route.GetPath(routeIndex);
                }
            }
        }
    }

    private IEnumerator AttackingAI()
    {
        while (true)
        {
            yield return new WaitForSeconds(preShotDelay);
            
            weapon.TryShoot();

            yield return new WaitForSeconds(postShotDelay);

            target = forwardCast.GetTarget(hostileTeams);

            if (target == null)
            {
                SetState(State.Alerted);
                yield break;
            }
        }
    }

    private void ResetAlertLook()
    {
        waitDuration = alertLookDuration;
        alertCurrentLookNum = alertLookNum;
    }

    private IEnumerator AlertedAI()
    {
        ResetAlertLook();
        while (true)
        {
            target = forwardCast.GetTarget(hostileTeams);
            if (target != null)
            {
                SetState(State.Attacking);
                yield break;
            }

            target = rightCast.GetTarget(hostileTeams);
            if (target != null)
            {
                Rotate(1);
                ResetAlertLook();
                continue;
            }

            target = leftCast.GetTarget(hostileTeams);
            if (target != null)
            {
                Rotate(-1);
                ResetAlertLook();
                continue;
            }
            
            float time = Time.time;
            yield return new WaitForFixedUpdate();
            var dt = Time.time - time;

            waitDuration -= dt;

            if (waitDuration <= 0f)
            {
                if (alertCurrentLookNum <= 0)
                {
                    SetState(State.Warned);
                    yield break;
                }

                int dir = Random.value > .5f ? 1 : -1;
                Rotate(dir * Random.Range(1, 2 + 1));
                
                waitDuration = alertLookDuration;
                alertCurrentLookNum--;
            }
        }
    }

    private IEnumerator WarnedAI()
    {
        waitDuration = distractedLookDuration;
        alertCurrentLookNum = 1;
        while (true)
        {
            if (forwardCast.GetTarget(hostileTeams) != null)
            
            target = forwardCast.GetTarget(hostileTeams);
            if (target != null)
            {
                SetState(State.Attacking);
                yield break;
            }

            target = rightCast.GetTarget(hostileTeams);
            if (target != null)
            {
                Rotate(1);
                SetState(State.Attacking);
                continue;
            }

            target = leftCast.GetTarget(hostileTeams);
            if (target != null)
            {
                Rotate(-1);
                SetState(State.Attacking);
                continue;
            }
            
            float time = Time.time;
            yield return new WaitForFixedUpdate();
            var dt = Time.time - time;

            waitDuration -= dt;

            if (waitDuration <= 0f)
            {
                if (alertCurrentLookNum <= 0)
                {
                    SetState(State.Default);
                    yield break;
                }

                int dir = Random.value > .5f ? 1 : -1;
                Rotate(dir * Random.Range(1, 1 + 1));
                
                waitDuration = alertLookDuration;
                alertCurrentLookNum--;
            }
        }
    }

    private void Rotate(int directionSteps)
    {
        var angles = transform.localEulerAngles;
        angles.y += directionSteps * 45f;
        transform.localEulerAngles = angles;
    }

    private IEnumerator AI()
    {
        yield return null;
        while (true)
        {
            if (state == State.Default)
                yield return PassiveAI();

            if (state == State.Attacking)
                yield return AttackingAI();

            if (state == State.Alerted)
                yield return AlertedAI();
            
            if (state == State.Warned)
                yield return WarnedAI();
        }
    }
}
