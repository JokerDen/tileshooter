using UnityEngine;

public class Pointable : MonoBehaviour
{

    public void SetOver()
    {
        BroadcastMessage("OnPointerOver", SendMessageOptions.DontRequireReceiver);
    }

    public void SetOut()
    {
        BroadcastMessage("OnPointerOut", SendMessageOptions.DontRequireReceiver);
    }

    public void SetDown()
    {
        BroadcastMessage("OnPointerDown", SendMessageOptions.DontRequireReceiver);
    }

    public void SetOverHit(RaycastHit hit)
    {
        BroadcastMessage("UpdateHit", hit, SendMessageOptions.DontRequireReceiver);
    }
}
