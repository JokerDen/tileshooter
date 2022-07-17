using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float radius;
    public float speed;
    public float maxDistance;

    private float distance;

    public LayerMask hitMask;
    public int[] hitTeams;

    public ParticleSystem finish;

    public int damage;
    public bool constantDamage;

    private List<Collider> hitted = new List<Collider>();

    public Fire fire;

    private bool IsValidHit(Damageable damageable)
    {
        if (damageable == null) return false;

        if (damageable.teams.Length > 0)
        {
            foreach (var team in damageable.teams)
            {
                if (hitTeams.Contains(team))
                    return true;
            }

            return false;
        }

        return true;
    }

    private void Update()
    {
        var hits = Physics.OverlapSphere(transform.position, radius, hitMask);
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            if (damage <= 0) return;
            if (hitted.Contains(hit)) continue;
            
            hitted.Add(hit);
            
            var damageable = hit.GetComponentInParent<Damageable>();
            if (IsValidHit(damageable))
                Hit(damageable);

            if (damage <= 0)
            {
                Finish();
                return;
            }
        }

        var step = speed * Time.deltaTime;
        step = Mathf.Min(step, maxDistance - distance);
        var pos = transform.position + transform.forward * step;

        /*hits = Physics.OverlapCapsule(transform.position, pos, radius, hitMask);
        // Debug.Log($"capsule {hits.Length}");
        }*/
        
        var ray = new Ray(transform.position, transform.forward);
        var rayHits = Physics.SphereCastAll(ray, radius, step, hitMask);
        foreach (var rayHit in rayHits)
        {
            var hit = rayHit.collider;
            if (hitted.Contains(hit)) continue;

            hitted.Add(hit);
            var damageable = hit.GetComponentInParent<Damageable>();
            if (IsValidHit(damageable))
                Hit(damageable);
            
            if (damage <= 0)
            {
                pos = rayHit.point + radius * rayHit.normal;
                transform.position = pos;
                Finish();
                return;
            }
        }
            
        transform.position = pos;
        distance += step;
        if (distance >= maxDistance)
        {
            Finish();
        }
    }

    private void Hit(Damageable target)
    {
        var hit = Mathf.Min(damage, target.health);
        if (!constantDamage)
            damage -= hit;
        if (fire != null)
        {
            Debug.Log("fire hit");
            var item = Instantiate(fire, target.transform.position, Quaternion.identity, target.transform);
            item.target = target;
            item.gameObject.SetActive(true);
        }
            
        target.Hit(hit);
    }

    private void Finish()
    {
        if (finish != null)
        {
            finish.transform.SetParent(null);
            finish.Play();
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
