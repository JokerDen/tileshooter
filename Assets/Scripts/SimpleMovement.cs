using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode downKey;
    public KeyCode upKey;

    public InputPressable holdDirection;
    public InputPressable running;

    public float speed;
    public float runSpeed;

    public Rigidbody body;

    private void FixedUpdate()
    {
        // body.velocity = Vector3.zero;
    }

    void Update()
    {
        Vector3 movement = Vector2.zero;

        if (Input.GetKey(leftKey))
            movement.x -= 1;
        if (Input.GetKey(rightKey))
            movement.x += 1;
        
        if (Input.GetKey(upKey))
            movement.z += 1;
        if (Input.GetKey(downKey))
            movement.z -= 1;

        if (movement.magnitude > 0f)
        {
            // Physics.BoxCast()

            var thisSpeed = running.IsPressing() ? runSpeed : speed;
            var pos = transform.position + movement.normalized * (thisSpeed * Time.deltaTime);
            
            if (!holdDirection.IsPressing())
                transform.LookAt(pos);
            
            transform.position = pos;
            // body.for
        }
    }
}
