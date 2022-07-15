using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    private LateFollower cam;
    
    void Start()
    {
        cam = FindObjectOfType<LateFollower>();
        cam.AddTarget(transform);
    }

    private void OnDestroy()
    {
        cam.RemoveTarget(transform);
    }
}
