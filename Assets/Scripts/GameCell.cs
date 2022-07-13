using System.Collections.Generic;
using UnityEngine;

public class GameCell : MonoBehaviour
{
    public Vector3Int Pos;
    public GameField Parent;

    public CellCursor onOver;

    public CellContent content = new CellEmpty();

    public GameObject empty;
    public GameObject panelsPrefab;
    public RoadBlock roads;

    private List<GameObject> panelBlocks = new List<GameObject>();

    private void OnPointerOver()
    {
        onOver.Show(Parent.provider.awaiting);
    }

    private void OnPointerOut()
    {
        onOver.Hide();
    }

    private void OnPointerDown()
    {
        Parent.Select(this);
    }

    public bool Place(CellContent placeContent)
    {
        if (content is CellBlocks)
        {
            if (placeContent is CellBlocks)
            {
                var panels = Instantiate(panelsPrefab, transform.position + Vector3.up * 5f * panelBlocks.Count, Quaternion.identity, transform);
                panelBlocks.Add(panels);
                return true;
            }

            if (placeContent is CellEmpty)
            {
                var last = panelBlocks[panelBlocks.Count - 1];
                panelBlocks.Remove(last);
                Destroy(last);

                if (panelBlocks.Count == 0)
                {
                    content = placeContent;
                    empty.SetActive(true);
                }
                return true;
            }

            return false;
        }

        if (content is CellRoad)
        {
            if (placeContent is CellEmpty)
            {
                roads.Hide();
                content = placeContent;
                empty.SetActive(true);
                return true;
            }

            return false;
        }
        
        if (!(content is CellEmpty))
            return false;

        if (placeContent is CellBlocks)
        {
            empty.SetActive(false);
            var panels = Instantiate(panelsPrefab, transform.position, Quaternion.identity, transform);
            panelBlocks.Add(panels);
            content = placeContent;
            return true;
        }

        if (placeContent is CellRoad)
        {
            empty.SetActive(false);
            roads.Show();
            content = placeContent;
            return true;
        }

        return false;
    }

    public void UpdateRoads()
    {
        var top = Parent.GetCell(Pos + Vector3Int.forward);
        var right = Parent.GetCell(Pos + Vector3Int.right);
        var bottom = Parent.GetCell(Pos + Vector3Int.back);
        var left = Parent.GetCell(Pos + Vector3Int.left);

        roads.UpdateConnections(new GameCell[] { top, right, bottom, left });
    }

    public void SetBuilding(GameBuilding building)
    {
        content = new CellBuilding(building);
        foreach (var block in panelBlocks)
        {
            Destroy(block);
        }
    }

    public bool IsPossibleToPlace(CellContent content)
    {
        if (content == null) return false;

        if (this.content is CellEmpty)
            return !(content is CellEmpty);
        
        return true;
    }
}
