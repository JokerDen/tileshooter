using UnityEngine;

public class StageController : MonoBehaviour
{
    public string startStage;
    public int startIndex;
    
    public InteractibleEvent onTargetChanged = new InteractibleEvent();

    private GameObject current;

    private void Start()
    {
        Load(startStage, startIndex);
    }

    public void Load(string stageResource, int stateIndex)
    {
        if (current != null)
        {
            Destroy(current);
            current = null;
        }
        
        var stage = Resources.Load<GameObject>(stageResource);
        current = Instantiate(stage, transform);

        var state = current.GetComponent<StartState>();
        if (state != null)
            state.SetState(stateIndex);
    }
}
