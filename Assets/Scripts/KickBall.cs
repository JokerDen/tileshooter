using System;
using UnityEngine;
using UnityEngine.AI;

public class KickBall : MonoBehaviour
{
    private NavMeshAgent[] agents;

    public Rigidbody ball;
    public float ballRadius;

    public float kickForce;
    public float upForce;

    public NavMeshAgent lastTouch;
    public float touchCooldown;
    private float lastTouchTime;

    public CollisionSound kickSound;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        var agent = collision.collider.GetComponent<NavMeshAgent>();
        // Debug.Log("Collision enter");
        if (agent == null) return;
        
        if (agent == lastTouch && Time.time - lastTouchTime < touchCooldown) return;
        
        // Debug.Log("Collision kick");

        var vel = agent.velocity.magnitude;
        vel /= agent.speed;
            
        kickSound.Play();
            
        lastTouch = agent;
        lastTouchTime = Time.time;

        var diff = ball.position - collision.GetContact(0).point;
        diff.y = 0f;
            
        var kick = diff * kickForce;
        kick.y = upForce;
        var ballVel = kick.normalized * ball.velocity.magnitude;
        ballVel += kick * vel;
        var max = new Vector2(kickForce, upForce).magnitude;
        if (ballVel.magnitude > max)
        {
            ballVel = ballVel.normalized * max;
        }
            
        ball.velocity = ballVel;
    }

    private void FixedUpdate()
    {
        return;
        if (agents == null || agents.Length == 0)
            agents = FindObjectsOfType<NavMeshAgent>();
        
        if (agents == null) return;
        
        var pos = ball.position;
        var hPos = pos;
        hPos.y = 0f;

        foreach (var agent in agents)
        {
            var agentPos = agent.transform.position;
            if (agent == lastTouch && Time.time - lastTouchTime < touchCooldown)
            {
                continue;
            }

            var vap = agentPos;
            vap.y = 0f;

            var diff = vap - hPos;
            
            if (diff.magnitude < agent.radius + ballRadius)
            {
                var vel = agent.velocity.magnitude;
                vel /= agent.speed;
                
                kickSound.Play();
                
                lastTouch = agent;
                lastTouchTime = Time.time;
                
                var kick = -diff * kickForce;
                kick.y = upForce;
                var ballVel = kick.normalized * ball.velocity.magnitude;
                ballVel += kick * vel;
                var max = new Vector2(kickForce, upForce).magnitude;
                if (ballVel.magnitude > max)
                {
                    ballVel = ballVel.normalized * max;
                }
                
                ball.velocity = ballVel;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ball.position, ballRadius);
    }
}
