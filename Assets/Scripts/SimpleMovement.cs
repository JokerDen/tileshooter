using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public InputPressable upInput;
    public InputPressable rightInput;
    public InputPressable downInput;
    public InputPressable leftInput;
    
    public InputPressable holdDirectionInput;
    public InputPressable runInput;
    
    public LayerMask castObstacles;

    private Vector3 movement = Vector3.zero;

    public CharacterMovement characterMovement;

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
        movement.x = GetAxisInput(rightInput, leftInput);
        movement.z = GetAxisInput(upInput, downInput);

        // var move = Physics.gravity * Time.deltaTime;
        // characterMovement.UpdateMoveDirection(movement, runInput.IsPressing());
        characterMovement.running = runInput.IsPressing();
        characterMovement.MoveToPoint(transform.position + Vector3.ClampMagnitude(movement, 1f));
        characterMovement.UpdateMove();
        
        if (movement.magnitude > 0f && !holdDirectionInput.IsPressing())
            transform.LookAt(transform.position + movement);

        /*if (movement.magnitude > 0f)
        {
            var thisSpeed = runInput.IsPressing() ? runSpeed : speed;
            var thisStep = thisSpeed * Time.deltaTime;
            var thisDiff = movement.normalized * thisStep;

            float radius = .5f;
            float offset = .1f;
            float offsetRadius = radius + .1f;
            Vector3 right = transform.right * radius;
            
            RaycastHit hit;
            var pos = transform.position;
            
            /*if (Physics.Raycast(transform.position, thisDiff.normalized, out hit, offsetRadius + thisDiff.magnitude,
                    castObstacles))
            // if (Physics.SphereCast(transform.position, radius / 2f, movement.normalized, out hit, thisStep + radius / 2f, castObstacles))
            {
                // pos = hit.point + (radius / 2f) * hit.normal - thisDiff.normalized * (radius / 4f);
                // pos = hit.point + radius  * hit.normal;
                // pos = transform.position + (hit.point - transform.position - thisDiff.normalized * radius);
                pos = hit.point - thisDiff.normalized * offsetRadius;
            } else if (Physics.Raycast(transform.position + right, thisDiff.normalized, out hit,
                           offset + thisDiff.magnitude, castObstacles))
            {
                pos = hit.point - thisDiff.normalized * offset - right;
            } else if (Physics.Raycast(transform.position - right, thisDiff.normalized, out hit,
                           offset + thisDiff.magnitude, castObstacles))
            {
                pos = hit.point - thisDiff.normalized * offset + right;
            }
            else#1#
                pos += thisDiff;
            
            if (!holdDirectionInput.IsPressing())
                transform.LookAt(transform.position + thisDiff);

            // character.Move(pos);
            // transform.position = pos;
            // character.Move(thisDiff);
            move += thisDiff;
        }*/
    }
}
