using System;
using DG.Tweening;
using UnityEngine;

public class ShadowCaster : MonoBehaviour
{
    // public Transform caster;
    public Transform shadow;
    public Vector3 offset;
    public float maxDistance = 100f;

    private void OnEnable()
    {
        var scale = shadow.localScale;
        shadow.localScale = Vector3.zero;
        shadow.DOScale(scale, 0.25f);
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, maxDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            shadow.position = hit.point + offset;
        }
    }
}
