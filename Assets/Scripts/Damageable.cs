using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int health;

    public ParticleSystem hitEffect;

    public bool invulnerable;
    public void Hit(int damage)
    {
        if (invulnerable) return;
        
        health -= damage;
        
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
            Destroy(gameObject);
        }
    }
}
