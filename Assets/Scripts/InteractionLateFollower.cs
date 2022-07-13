using UnityEngine;

public class InteractionLateFollower : MonoBehaviour
{
    public Interactible target;
    public LateFollower follower;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning("no target to follow");
            return;
        }
        
        target.onStartInteractWith.AddListener(AddFollow);
        target.onEndInteractWith.AddListener(RemoveFollow);
    }

    private void RemoveFollow(Interactible arg0)
    {
        follower.RemoveTarget(arg0.target);
    }

    private void AddFollow(Interactible arg0)
    {
        follower.AddTarget(arg0.target);
    }
}
