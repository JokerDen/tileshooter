using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float interval;
    
    public GameObject[] spawnSamples;

    private bool isShooting;
    private float lastShootTime;

    public Burstable burst;
    public LocalMovable movable;

    private void Update()
    {
        if (isShooting)
            TryShoot();
    }

    public void TryShoot()
    {
        if (Time.time - lastShootTime < interval) return;

        lastShootTime = Time.time;

        foreach (var spawnSample in spawnSamples)
        {
            var item = Instantiate(spawnSample, spawnSample.transform.position, spawnSample.transform.rotation);
            item.SetActive(true);
        }

        burst.Play();
        movable.PlayFromOffset();
    }

    public void SetShooting(bool value)
    {
        if (isShooting == value) return;

        var wasShooting = isShooting;
        isShooting = value;
        if (!wasShooting && isShooting)
            TryShoot();
    }
}
