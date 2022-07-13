using activity;
using UnityEngine;

public class SetPlayerActor : MonoBehaviour
{
    void Start()
    {
        var actor = GetComponent<Actor>();
        actor.character = WorldManager.instance.CreateCharacter();
        actor.character.AddActivity(new ControllableMovementActivity());
    }
}
