using UnityEngine;

public class DevTool : MonoBehaviour
{
    public static DevTool instance;
    public bool activated;

    private void Awake()
    {
        instance = this;
        if (Application.isEditor)
            activated = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.LeftShift))
            activated = true;
    }
}
