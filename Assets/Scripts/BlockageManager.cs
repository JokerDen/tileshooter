using System.Collections.Generic;
using UnityEngine;

public class BlockageManager : MonoBehaviour
{
    private static BlockageManager current;
    public static BlockageManager Active => current;

    private void Awake()
    {
        current = this;
    }

    public GameGrid grid;
    public QueueProvider provider;

    public List<GameTile> placedTiles = new List<GameTile>();

    public bool OnGrid(Vector3 pos)
    {
        var gridPos = grid.GetClosestPos(pos);
        return Vector3.Distance(pos, gridPos) < grid.cellSize / 2f;
    }

    public void AddTilesAtPos(Vector3 position, List<GameTile> target)
    {
        foreach (var tile in placedTiles)
            if (Vector3.Distance(position, tile.transform.position) < grid.cellSize / 2f)
                target.Add(tile);
    }
}