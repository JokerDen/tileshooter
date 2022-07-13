using UnityEngine;

public class InteractibleAwaiter : MonoBehaviour
{
    public Interactible[] targets;
    public Trigger trigger;
    public float radius;

    public float awaitTime = 1f;
    private float currentTime = 0f;

    private void Update()
    {
        if (currentTime >= awaitTime) return;

        var pos = transform.position;
        for (int i = 0; i < targets.Length; i++)
        {
            var interactible = targets[i].transform;
            float distance = Vector3.Distance(pos, interactible.position);
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
