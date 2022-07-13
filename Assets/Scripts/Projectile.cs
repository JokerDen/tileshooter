using System;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float radius;
    public float speed;
    public float maxDistance;

    private float distance;

    public LayerMask hitMask;

    public ParticleSystem finish;

    public int damage;

    private List<Collider> hitted = new List<Collider>();

    /*private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        body.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (damage <= 0) return;
        if (hitted.Contains(collision.collider)) return;
        
        hitted.Add(collision.collider);
        var damageable = collision.collider.GetComponentInParent<Damageable>();
        if (damageable != null)
            Hit(damageable);

        if (damage <= 0)
        {
            /*var contact = collision.GetContact(0);
            Vector3 center = contact.point + radius * contact.normal;
            transform.position = center;#1#
            Finish();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (damage <= 0) return;
        if (hitted.Contains(other)) return;
        
        hitted.Add(other);
        var damageable = other.GetComponentInParent<Damageable>();
        if (damageable != null)
            Hit(damageable);

        if (damage <= 0)
        {
            /*var contact = collision.GetContact(0);
            Vector3 center = contact.point + radius * contact.normal;
            transform.position = center;#1#
            Finish();
        }
    }*/

    private void Update()
    {
        // return;
        var hits = Physics.OverlapSphere(transform.position, radius, hitMask);
        // Debug.Log($"overlap {hits.Length}");
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            if (damage <= 0) return;
            if (hitted.Contains(hit)) continue;
            
            hitted.Add(hit);
            
            var damageable = hit.GetComponentInParent<Damageable>();
            if (damageable != null)
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
        for (int i = 0; i < hits.Length; i++)
        {
            var hit = hits[i];
            if (damage <= 0) return;
            if (hitted.Contains(hit)) continue;
            
            hitted.Add(hit);
            
            var damageable = hit.GetComponentInParent<Damageable>();
            if (damageable != null)
                Hit(damageable);

            if (damage <= 0)
            {
                pos = rayHit.point + radius * rayHit.normal;
                transform.position = pos;
                Finish();
                return;
            }
        }*/
        
        var ray = new Ray(transform.position, transform.forward);
        var rayHits = Physics.SphereCastAll(ray, radius, step, hitMask);
        foreach (var rayHit in rayHits)
        {
            var hit = rayHit.collider;
            if (hitted.Contains(hit)) continue;

            hitted.Add(hit);
            var damageable = hit.GetComponentInParent<Damageable>();
            if (damageable != null)
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
        damage -= hit;
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
