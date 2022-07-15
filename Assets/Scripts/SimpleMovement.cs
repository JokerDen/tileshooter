using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public InputPressable upInput;
    public InputPressable rightInput;
    public InputPressable downInput;
    public InputPressable leftInput;
    
    public InputPressable holdDirectionInput;
    public InputPressable runInput;

    public float speed;
    public float runSpeed;
    
    private int GetMoveInput(InputPressable input, int diff)
    {
        return input.IsPressing() ? diff : 0;
    }

    private int GetAxisInput(InputPressable positive, InputPressable negative)
    {
        return GetMoveInput(positive, 1) + GetMoveInput(negative, -1);
    }

    void Update()
    {
        Vector3 movement = new Vector3(
            GetAxisInput(rightInput, leftInput),
            0,
            GetAxisInput(upInput, downInput)
        );

        if (movement.magnitude > 0f)
        {
            var thisSpeed = runInput.IsPressing() ? runSpeed : speed;
            var pos = transform.position + movement.normalized * (thisSpeed * Time.deltaTime);
            
            if (!holdDirectionInput.IsPressing())
                transform.LookAt(pos);
            
            transform.position = pos;
        }
    }
}
