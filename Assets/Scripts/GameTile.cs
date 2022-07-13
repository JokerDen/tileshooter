using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    public enum TileType
    {
        Undefined,
        NotBlocked,
        Road,
        Block
    }

    public GameObject forbidden;
    public GameObject permitted; 

    public TileType tileType;

    private List<GameTile> buffer = new List<GameTile>();
    
    public bool Validate()
    {
        var pos = transform.position;

        bool requireOnGrid = tileType == TileType.Road || tileType == TileType.Block;
        if (requireOnGrid && !BlockageManager.Active.OnGrid(pos))
        {
            SetPermitted(false);
            return false;
        }
        
        buffer.Clear();
        BlockageManager.Active.AddTilesAtPos(pos, buffer);
        foreach (var tile in buffer)
        {
            var otherType = tile.tileType;
            if (tileType == TileType.Block && (otherType == TileType.Block || otherType == TileType.Road))
            {
                SetPermitted(false);
                return false;
            }

            if (tileType == TileType.Road && otherType == TileType.Block)
            {
                SetPermitted(false);
                return false;
            }
            
            if (tileType == TileType.NotBlocked && otherType == TileType.Block)
            {
                SetPermitted(false);
                return false;
            }
        }
        
        SetPermitted(true);
        return true;
    }
    
    public void SetPermitted(bool value)
    {
        permitted.SetActive(value);
        forbidden.SetActive(!value);
    }
}