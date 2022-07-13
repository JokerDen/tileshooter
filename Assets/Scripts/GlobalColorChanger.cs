using UnityEngine;

public class GlobalColorChanger : MonoBehaviour
{
    public Material[] colorMaterials;

    private int colorIdx;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var color = Random.ColorHSV();
            ApplyColor(color);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            colorIdx++;
            if (colorIdx >= colorMaterials.Length)
                colorIdx = 0;
            ApplyColor(colorMaterials[colorIdx].color);
        }
    }

    public void ApplyColor(Color color)
    {
        var cameras = FindObjectsOfType<Camera>();
        foreach (var cam in cameras)
            cam.backgroundColor = color;

        RenderSettings.fogColor = color;
    }
}
