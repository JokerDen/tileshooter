using UnityEngine;

public class Fire : MonoBehaviour
{
    public Damageable target;

    public int damage;

    public float interval;

    private float nextDamageTime;

    private void Start()
    {
        nextDamageTime = Time.time + interval;
    }

    private void Update()
    {
        if (Time.time >= nextDamageTime)
        {
            nextDamageTime += interval;
            target.Hit(damage);
        }
    }
}
