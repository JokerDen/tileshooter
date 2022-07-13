using UnityEngine;

public class FrameRator : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 0.02f;
    }
}
