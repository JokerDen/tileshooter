using UnityEngine;

public class InteractibleSpawn : MonoBehaviour
{
    public GameObject prefab;

    public Transform spawnPos;

    private Interactible interactible;

    private void Start()
    {
        interactible = GetComponent<Interactible>();
        interactible.onStartInteractWith.AddListener(Spawn);
    }

    private void Spawn(Interactible arg0)
    {
        var item = Instantiate(prefab, spawnPos.position, spawnPos.rotation, transform);
        arg0.EndInteraction(interactible);
        // interactible.EndInteraction(arg0);
    }
}
