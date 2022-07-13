using UnityEngine;

namespace activity
{
    public class ControllableMovementActivity : Activity
    {
        protected override void ExecuteOnStage(Actor actor, float deltaTime)
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            actor.agent.velocity = new Vector3(h, 0f, v) * actor.agent.speed;
            
            
            /*var angle = Vector3.SignedAngle(transform.forward, lookDirection, Vector3.up);
            var angleStep = agent.angularSpeed * Time.deltaTime;
            angle = Mathf.Clamp(angle, -angleStep, angleStep);
            transform.Rotate(Vector3.up, angle);*/
        }
    }
}