using UnityEngine;

public class BallAwaiter : MonoBehaviour
{
    public KickBall[] balls;
    public Trigger trigger;
    public float radius;

    public float awaitTime = 1f;
    private float currentTime = 0f;

    private void Update()
    {
        if (currentTime >= awaitTime) return;

        var pos = transform.position;
        for (int i = 0; i < balls.Length; i++)
        {
            var kickBall = balls[i];
            float distance = Vector3.Distance(pos, kickBall.ball.transform.position);
            if (distance > radius)
                return;
        }

        currentTime += Time.deltaTime;
        if (currentTime >= awaitTime)
        {
            trigger.Trig();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
