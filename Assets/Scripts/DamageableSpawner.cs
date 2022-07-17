using UnityEngine;

public class DamageableSpawner : MonoBehaviour
{
    public GameObject[] random;
    
    void Start()
    {
        GetComponent<Damageable>().onDestroy.AddListener(Spawn);
    }

    private void Spawn()
    {
        if (random.Length < 1) return;

        var prefab = random[Random.Range(0, random.Length)];

        var item = Instantiate(prefab, transform.position, Quaternion.identity);
        item.SetActive(true);
    }
}
