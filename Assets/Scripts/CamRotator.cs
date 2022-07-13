using System;
using UnityEngine;

public class CamRotator : MonoBehaviour
{
    public Transform around;
    public Transform lean;
    public Camera cam;

    private Vector3 lastPos;

    public float aroundSensitivity = 1f;
    public float leanSensitivity = 1f;
    public float zoomSensitivity = 1f;
    public float fovSensitivity = 1f;
    
    public float minZoom;
    public float maxZoom;
    
    public float minFov;
    public float maxFov;

    public float minLean;
    public float maxLean;
    private float leanAngle;

    public static CamRotator enabled;

    private void Start()
    {
        leanAngle = lean.localEulerAngles.x;
    }

    private void OnEnable()
    {
        enabled = this;
    }

    private void Update()
    {
        if (!DevTool.instance.activated) return;
        
        if (Input.GetMouseButtonDown(1))
            lastPos = Input.mousePosition;

        var mouseScrollDelta = Input.mouseScrollDelta;
        if (!Input.GetMouseButton(1))
        {
            var scroll = mouseScrollDelta * zoomSensitivity;
            var camPos = cam.transform.localPosition;
            camPos.z = Mathf.Clamp(camPos.z + scroll.y, -maxZoom, -minZoom);
            cam.transform.localPosition = camPos;
            
            return;
        }
        else
        {
            cam.fieldOfView = Mathf.Clamp(cam.fieldOfView + mouseScrollDelta.y * fovSensitivity, minFov, maxFov);
        }

        var mousePosition = Input.mousePosition;
        var delta = (lastPos - mousePosition) * Time.deltaTime;
        lastPos = mousePosition;
        
        var angles = around.localEulerAngles;
        angles.y += -delta.x * aroundSensitivity;
        around.localEulerAngles = angles;

        leanAngle = Mathf.Clamp(leanAngle + delta.y * leanSensitivity, minLean, maxLean);
        angles = lean.localEulerAngles;
        angles.x = leanAngle;
        lean.localEulerAngles = angles;
    }
}
