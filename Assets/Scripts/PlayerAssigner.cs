using UnityEngine;

public class PlayerAssigner : MonoBehaviour
{
    
    void Start()
    {
        var stage = GetComponentInParent<StageController>();
        var interactible = GetComponent<Interactible>();
        stage.onTargetChanged.Invoke(interactible);

        var input = FindObjectOfType<CustomInput>();
        GetComponent<MovementController>().input = input;
        interactible.input = input;
    }
}
