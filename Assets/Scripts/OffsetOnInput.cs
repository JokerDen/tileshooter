using UnityEngine;

public class OffsetOnInput : MonoBehaviour
{
    public Vector3 localOffset;
    public InputPressable input;

    private Vector3 startPos;
    
    void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        if (input.IsDown())
            transform.localPosition = startPos + localOffset;
        if (input.IsUp())
            transform.localPosition = startPos;
    }
}
