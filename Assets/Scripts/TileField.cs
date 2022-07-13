using UnityEngine;

public class TileField : MonoBehaviour
{
    private bool isOver;

    private Vector3 pos;

    private void OnDrawGizmos()
    {
        if (isOver)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(pos, .1f);
        }
    }

    void OnPointerOver()
    {
        isOver = true;
    }

    void OnPointerOut()
    {
        isOver = false;
        
        if (BlockageManager.Active.provider.current != null)
            BlockageManager.Active.provider.current.Hide();
    }

    void OnPointerDown()
    {
        if (BlockageManager.Active.provider.current == null) return;
        if (!BlockageManager.Active.provider.current.isValid) return;

        BlockageManager.Active.provider.BuildCurrent();
    }
    
    void UpdateHit(RaycastHit hit)
    {
        pos = hit.point;
        
        if (BlockageManager.Active.provider.current == null) return;

        BlockageManager.Active.provider.current.ShowOnPos(pos);
    }
}