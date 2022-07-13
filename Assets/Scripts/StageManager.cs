using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public Actor[] actors;

    private void Awake()
    {
        instance = this;

        actors = FindObjectsOfType<Actor>();
    }

    public Actor GetActor(CharacterData character)
    {
        foreach (var actor in actors)
            if (actor.character == character)
                return actor;

        return null;
    }
}