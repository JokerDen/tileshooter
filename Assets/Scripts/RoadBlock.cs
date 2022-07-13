using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public RoadConnection[] connections;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void UpdateConnections(GameCell[] neighbours)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            bool hasForward = IsRoad(neighbours[i]);
            bool hasRight = IsRoad(i < connections.Length - 1 ? neighbours[i + 1] : neighbours[0]);

            connections[i].SetNeighbours(hasForward, hasRight);
        }
    }

    private bool IsRoad(GameCell cell)
    {
        return cell != null && cell.content is CellRoad;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
