using UnityEngine;

public class ColorSwitcher : MonoBehaviour
{
    public Material colorSource;
    public Color color;
    public MeshRenderer[] colored;
    public Camera targetCam;

    private Interactible interactible;

    private void Start()
    {
        if (colorSource != null)
        {
            // var emiColor = colorSource.GetColor("_EmissionColor");
            // color = emiColor;
            color = colorSource.color;
        }
        
        foreach (var col in colored)
        {
            col.material.color = color;
        }

        interactible = GetComponent<Interactible>();
        interactible.onStartInteractWith.AddListener(SwitchColor);
    }

    private void SwitchColor(Interactible arg0)
    {
        targetCam.backgroundColor = color;
        arg0.EndInteraction(interactible);
        interactible.EndInteraction(arg0);
    }
}
