using UnityEngine;

public class CastForward : MonoBehaviour
{
    public float distance;

    public Color gizmoColor;

    public float interval;
    public float amplitude;

    // private void OnDrawGizmosSelected()
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        var forward = transform.forward;
        var fromPos = transform.position;
        Gizmos.DrawLine(fromPos, fromPos + forward * distance);

        int sidesNum = Mathf.FloorToInt(amplitude / interval);
        for (int i = 0; i < sidesNum; i++)
        {
            var dir = Quaternion.Euler(0, (i + 1) * interval, 0) * forward;
            Gizmos.DrawLine(fromPos, fromPos + dir * distance);
            dir = Quaternion.Euler(0, (i + 1) * -interval, 0) * forward;
            Gizmos.DrawLine(fromPos, fromPos + dir * distance);
        }
    }
}
