using System;
using UnityEngine;

public class BackColorCaster : MonoBehaviour
{
    public Camera cam;
    public LayerMask castMask;
    public float castDistance = 100f;
    public float colorMultiplier = 1f;
    
    private MeshRenderer mr;
    private Material mat;
    
    public enum CastType
    {
        CamForward = 10,
        Down = 100
    }

    public CastType castType;

    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    private void Start()
    {
        Update();
    }

    private void Update()
    {
        if (cam == null)
            cam = CamRotator.enabled.cam;
        
        Color color = cam.backgroundColor;
        
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.forward);
        if (castType == CastType.Down)
            ray.direction = Vector3.down;
        if (castType == CastType.CamForward)
            ray.direction = transform.position - cam.transform.position;
        
        if (Physics.Raycast(ray, out hit, castDistance, castMask, QueryTriggerInteraction.Ignore))
        {
            var meshR = hit.collider.GetComponent<MeshRenderer>();
            if (meshR != null)
                color = meshR.sharedMaterial.color;
        }

        // color *= 2f;
        // color.a = 1f;
        mr.material.SetColor("_Color", color);
        mr.material.SetColor("_BaseColor", color);
        // mr.material.SetColor("_Color", Color.black);
        // mr.material.SetColor("_BaseColor", Color.black);
        // mr.material.SetColor("_EmissionColor", color * colorMultiplier);
    }
}
