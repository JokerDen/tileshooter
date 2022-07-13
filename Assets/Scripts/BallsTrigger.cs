using UnityEngine;
using UnityEngine.AI;

public class BallsTrigger : MonoBehaviour
{
    public NavMeshAgent triggin;
    public SphereCollider trigger;
    public GameObject activate;
    
    void Update()
    {
        if (activate != null)
        {
            if (Vector3.Distance(triggin.transform.position, transform.position) < triggin.radius + trigger.radius)
            {
                activate.SetActive(true);
                activate = null;
            }
        }
    }
}
