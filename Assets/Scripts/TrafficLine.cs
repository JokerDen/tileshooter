using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrafficLine : MonoBehaviour
{
    public float distance;

    public float speed;

    public GameObject[] prefabs;

    private List<GameObject> items = new List<GameObject>();

    public float minIntervalTime;
    public float maxIntervalTime;

    private float nextSpawn;

    private List<GameObject> removeItems = new List<GameObject>();

    private void Update()
    {
        foreach (var item in items)
        {
            item.transform.position += item.transform.forward * speed * Time.deltaTime;
            if (item.transform.localPosition.z >= distance)
                removeItems.Add(item);
        }

        if (removeItems.Count > 0)
        {
            foreach (var item in removeItems)
            {
                Destroy(item);
                items.Remove(item);
            }
            
            removeItems.Clear();
        }
        
        if (Time.time >= nextSpawn)
        {
            nextSpawn = Time.time + Random.Range(minIntervalTime, maxIntervalTime);

            var prefab = prefabs[Random.Range(0, prefabs.Length)];

            var item = Instantiate(prefab, transform.position, transform.rotation, transform);
            item.SetActive(true);
            items.Add(item);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * distance);
    }
}
