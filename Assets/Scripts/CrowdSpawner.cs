using System.Collections;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
    public ActorSpawnPosition[] positionsA;
    public ActorSpawnPosition[] positionsB;

    public float interval;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
        
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (Random.value > .5f)
                Spawn(positionsA, positionsB);
            else
                Spawn(positionsB, positionsA);
            
            yield return new WaitForSeconds(interval);
        }
    }

    private void Spawn(ActorSpawnPosition[] fromPositions, ActorSpawnPosition[] toPositions)
    {
        var fromPos = fromPositions[Random.Range(0, fromPositions.Length)];
        var toPos = toPositions[Random.Range(0, toPositions.Length)];

        var actor = fromPos.SpawnActor();

        actor.MoveTo(toPos.transform, () => { Destroy(actor.gameObject); });
    }
}
