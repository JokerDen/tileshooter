using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(MaterialApplier))]
public class MaterialApplierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Apply"))
        {
            (target as MaterialApplier).ApplyMaterial();
            EditorUtility.SetDirty(target);
        }
    }
}
#endif

public class MaterialApplier : MonoBehaviour
{
    public Material material;
    public Material[] exceptionMaterials;

    public void ApplyMaterial()
    {
        var meshRenderers = GetComponentsInChildren<MeshRenderer>();
        
        foreach (var meshRenderer in meshRenderers)
        {
            if (IsException(meshRenderer.sharedMaterial)) continue;
            
            meshRenderer.sharedMaterial = material;
        }
    }

    private bool IsException(Material mat)
    {
        foreach (var exceptionMaterial in exceptionMaterials)
        {
            if (mat == exceptionMaterial)
                return true;
        }

        return false;
    }
}
