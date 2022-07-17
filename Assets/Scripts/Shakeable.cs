using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shakeable : MonoBehaviour
{
    public Vector3 amplitudePerStrength;

    private float currentStrength;

    public float moveSmooth;
    public float strengthSmooth;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void LateUpdate()
    {
        if (currentStrength > 0f)
            currentStrength = Mathf.Lerp(currentStrength, 0f, strengthSmooth * Time.deltaTime);

        var pos = startPos;
        pos.x += Random.Range(-amplitudePerStrength.x, amplitudePerStrength.x) * currentStrength;
        pos.y += Random.Range(-amplitudePerStrength.y, amplitudePerStrength.y) * currentStrength;
        pos.z += Random.Range(-amplitudePerStrength.z, amplitudePerStrength.z) * currentStrength;

        transform.localPosition = Vector3.Lerp(transform.localPosition, pos, moveSmooth * Time.deltaTime);
    }

    public void AddShake(float strength)
    {
        currentStrength += strength;
    }
}
