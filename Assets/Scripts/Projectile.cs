using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float radius;
    public float speed;
    public float maxDistance;

    private float distance;

    public LayerMask hitMask;

    public ParticleSystem finish;

    private void Update()
    {
        var hits = Physics.OverlapSphere(transform.position, radius, hitMask);
        if (hits.Length > 0)
        {
            Finish();
            return;
        }
        
        
        var step = speed * Time.deltaTime;
        step = Mathf.Min(step, maxDistance - distance);
        var pos = transform.position + transform.forward * step;

        RaycastHit hitInfo;
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, radius, out hitInfo, step, hitMask))
        {
            pos = hitInfo.point + radius * hitInfo.normal;
            transform.position = pos;
            Finish();
            return;
        }
            
        transform.position = pos;
        distance += step;
        if (distance >= maxDistance)
        {
            Finish();
        }
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
