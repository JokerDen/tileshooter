using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public float cellSize;

    public List<GameCell> cells = new List<GameCell>();

    public CellContentProvider provider;

    public BuildingProject[] projects;
    public List<GameBuilding> buildings;

    public int randomBlocksNum;

    private void Start()
    {
        var gameCells = GetComponentsInChildren<GameCell>();
        foreach (var cell in gameCells)
        {
            var pos = cell.transform.position;
            pos /= cellSize;
            cell.Pos = new Vector3Int(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
            cell.Parent = this;
            cells.Add(cell);
        }

        for (int i = 0; i < randomBlocksNum; i++)
            CreateRandomBlock();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            CreateRandomBlock();
    }

    private void CreateRandomBlock()
    {
        List<GameCell> possible = new List<GameCell>();
        foreach (var cell in cells)
            if (IsRandomAvailable(cell))
                possible.Add(cell);

        if (possible.Count > 0)
        {
            var rCell = possible[Random.Range(0, possible.Count)];
            rCell.Place(new CellBlocks());
        }
    }

    private bool IsRandomAvailable(GameCell rCell)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var pos = rCell.Pos;
                pos.x += -1 + i;
                pos.z += -1 + j;
                var cell = GetCell(pos);
                if (cell != null && !(cell.content is CellEmpty))
                    return false;
            }
        }

        return true;
    }

    public void Select(GameCell gameCell)
    {
        if (PlaceCell(gameCell, provider.awaiting))
            provider.Next();
    }

    private bool IsPossibleToPlace(GameCell target, CellContent content)
    {
        if (target == null || content == null) return false;
        
        foreach (var building in buildings)
            if (!building.IsPossibleToPlace(target, content))
                return false;

        if (!target.IsPossibleToPlace(content))
            return false;
    
        return true;
    }

    private bool PlaceCell(GameCell target, CellContent content)
    {
        if (IsPossibleToPlace(target, content) && target.Place(content))
        {
            TryBuilding(target);

            foreach (var cell in cells)
                cell.UpdateRoads();

            return true;
        }

        return false;
    }

    private void TryBuilding(GameCell gameCell)
    {
        foreach (var project in projects)
        {
            var building = project.TryBuild(gameCell);
            if (building != null)
            {
                buildings.Add(building);
                TryBuilding(gameCell);
                return;
            }
        }
    }

    public GameCell GetCell(Vector3Int pos)
    {
        foreach (var cell in cells)
            if (cell.Pos.Equals(pos))
                return cell;
        return null;
    }

    public bool PlaceRandom(CellContent content)
    {
        List<GameCell> possible = new List<GameCell>();
        foreach (var gameCell in cells)
            if (IsPossibleToPlace(gameCell, content))
                possible.Add(gameCell);

        if (possible.Count > 0)
        {
            var rIdx = Random.Range(0, possible.Count);
            return PlaceCell(possible[rIdx], content);
        }

        return false;
    }
}