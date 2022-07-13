using UnityEngine;

public class CellCursor : MonoBehaviour
{
    public GameObject roads;
    public GameObject block;
    public GameObject empty;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(CellContent target)
    {
        roads.SetActive(target is CellRoad);
        block.SetActive(target is CellBlocks);
        empty.SetActive(target is CellEmpty);
        
        gameObject.SetActive(true);
    }
}
