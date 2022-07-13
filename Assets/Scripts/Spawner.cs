using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float randomizePos;
    public GameObject prefab;

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && DevTool.instance.activated)
        {
            var item = Instantiate(prefab, transform.position + Random.insideUnitSphere * randomizePos, prefab.transform.rotation,
                prefab.transform.parent);
            item.SetActive(true);
        }
    }
}