using UnityEngine;

public class TileBuilding : MonoBehaviour
{
    private GameTile[] tiles;
    private Vector3[] tileLocalPositions;

    public bool isValid;

    public GameObject tilesContainer;
    public GameObject model;

    public void Hide()
    {
        isValid = true;
        gameObject.SetActive(false);
    }

    public void ShowOnPos(Vector3 pos)
    {

        if (tiles == null || tiles.Length == 0)
        {
            tiles = GetComponentsInChildren<GameTile>(true);
            tileLocalPositions = new Vector3[tiles.Length];
            for (int i = 0; i < tiles.Length; i++)
            {
                tileLocalPositions[i] = tiles[i].transform.localPosition;
            }
        }
        
        // transform.position = pos;
        transform.position = BlockageManager.Active.grid.GetClosestPos(pos);

        /*for (int i = 0; i < tiles.Length; i++)
        {
            var tileTransform = tiles[i].transform;
            var localPos = tileTransform.parent.TransformPoint(tileLocalPositions[i]);
            localPos = grid.GetClosestPos(localPos);
            tileTransform.position = localPos;
        }*/

        isValid = true;
        foreach (var tile in tiles)
        {
            if (!tile.Validate())
                isValid = false;
        }
        
        gameObject.SetActive(true);
    }

    public void Build()
    {
        foreach (var tile in tiles)
        {
            BlockageManager.Active.placedTiles.Add(tile);
        }
        
        tilesContainer.SetActive(false);
        model.SetActive(true);
        
        gameObject.SetActive(true);
    }
}
