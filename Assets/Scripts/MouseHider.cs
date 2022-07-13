using UnityEngine;

public class MouseHider : MonoBehaviour
{
    
    void Start()
    {
        if (Application.isEditor) return;
        Cursor.visible = false;
    }
}
