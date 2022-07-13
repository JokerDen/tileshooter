using Unity.Mathematics;
using UnityEngine;

public class ActorSpawnPosition : MonoBehaviour
{
    public Actor actorPrefab;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, .5f);
    }

    public Actor SpawnActor()
    {
        return Instantiate(actorPrefab, transform.position, quaternion.identity, transform);
    }
}
