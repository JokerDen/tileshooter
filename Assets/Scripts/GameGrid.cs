using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public float cellSize;

    public Vector2Int size;

    private void OnDrawGizmos()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector3 pos = GetCellCenter(i, j);
                
                Gizmos.color = Color.red;
                Gizmos.matrix = transform.localToWorldMatrix;
                Gizmos.DrawWireCube(pos, new Vector3(cellSize, 0f, cellSize));
            }
        }
    }

    private Vector3 GetCellCenter(int x, int y)
    {
        float w = size.x * cellSize;
        float h = size.y * cellSize;

        return new Vector3(
            -w * .5f + x * cellSize + cellSize * .5f,
            0,
            -h * .5f + y * cellSize + cellSize * .5f
        );
    }

    public Vector3 GetClosestPos(Vector3 pos)
    {
        Vector3 closestPos = GetCellCenter(0, 0);
        float closestDist = float.MaxValue;
        
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                var cellPos = GetCellCenter(i, j);
                float dist = Vector3.Distance(pos, cellPos);
                if (dist < closestDist)
                {
                    closestDist = dist;
                    closestPos = cellPos;
                }
            }
        }

        return closestPos;
    }
}
