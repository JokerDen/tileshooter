using UnityEngine;

public class QueueProvider : MonoBehaviour
{
    public TileBuilding[] buildings;

    public TileBuilding current;

    private TileBuilding[] hold;

    private TileBuilding[] next;

    private void Update()
    {
        if (current == null)
        {
            var rIdx = Random.Range(0, buildings.Length);
            var rPrefab = buildings[rIdx];

            current = Instantiate(rPrefab, rPrefab.transform.position,
                Quaternion.Euler(0, Random.Range(0, 4) * 90f, 0f));
            current.Hide();
        }
    }

    public void BuildCurrent()
    {
        current.Build();
        
        current = null;
    }
}
