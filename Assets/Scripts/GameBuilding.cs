using UnityEngine;

public class GameBuilding : MonoBehaviour
{
    public RequirementFormat format;
    public Vector3Int offset;

    public bool IsPossibleToPlace(GameCell gameCell, CellContent content)
    {
        foreach (var cellRequirement in format.reqs)
        {
            var pos = cellRequirement.pos + offset;
            if (gameCell.Pos.Equals(pos) && !cellRequirement.Fit(content))
                return false;
        }

        return true;
    }
}