using UnityEngine;

public class CustomInput : MonoBehaviour
{
    public Transform rotateTarget;

    public Vector3 movement;

    public bool locked;
    
    private void Update()
    {
        if (rotateTarget == null)
            rotateTarget = CamRotator.enabled.cam.transform;
        
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (locked)
            movement = Vector3.zero;

        var angleY = rotateTarget.eulerAngles.y;
        movement = Quaternion.AngleAxis(angleY, Vector3.up) * movement;
        if (movement.magnitude > 1f)
            movement.Normalize();
        // stage.InputMove(freedom == ContextualAction.Freedom.FreeMove ? input : Vector3.zero);
    }

    public bool IsAction()
    {
        return !locked && Input.GetKeyDown(KeyCode.E);
    }
}
