using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastForward : MonoBehaviour
{
    public float distance;

    public Color gizmoColor;

    public float interval;
    public float amplitude;

    private List<Vector3> castDirections = new List<Vector3>();

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

    public Damageable GetTarget(int[] teams)
    {
        castDirections.Clear();

        var forward = transform.forward * distance;
        castDirections.Add(forward);

        int sidesNum = Mathf.FloorToInt(amplitude / interval);
        for (int i = 0; i < sidesNum; i++)
        {
            var dir = Quaternion.Euler(0, (i + 1) * interval, 0) * forward;
            castDirections.Add(dir);
            dir = Quaternion.Euler(0, (i + 1) * -interval, 0) * forward;
            castDirections.Add(dir);
        }

        Ray ray = new Ray(transform.position, forward);
        RaycastHit hit;
        foreach (var direction in castDirections)
        {
            ray.direction = direction;
            if (Physics.Raycast(ray, out hit, direction.magnitude))
            {
                var target = hit.collider.GetComponentInParent<Damageable>();
                if (target != null)
                {
                    foreach (var targetTeam in target.teams)
                        if (teams.Contains(targetTeam))
                            return target;
                }
            }
        }
        
        return null;
    }
}
