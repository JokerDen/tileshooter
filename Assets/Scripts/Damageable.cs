using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public int health;
    public bool invulnerable;

    public ParticleSystem hitEffect;

    public int[] teams;

    public UnityEvent onHit;
    public UnityEvent onDestroy;
    
    public void Hit(int damage)
    {
        if (invulnerable) return;
        
        health -= damage;
        onHit.Invoke();
        
        if (health <= 0)
        {
            var colliders = GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
            {
                col.enabled = false;
            }

            if (hitEffect != null)
            {
                hitEffect.Play();
                hitEffect.transform.SetParent(null);
            }
            // var main = hitEffect.main;
            // main.stopAction = ParticleSystemStopAction.Destroy;
            onDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
