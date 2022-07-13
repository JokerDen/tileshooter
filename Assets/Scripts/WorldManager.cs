using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance;
    private void Awake()
    {
        instance = this;
    }

    private List<CharacterData> characters = new List<CharacterData>();

    public CharacterData CreateCharacter()
    {
        var character = new CharacterData();
        characters.Add(character);
        return character;
    }

    private void Update()
    {
        foreach (var character in characters)
        {
            character.SkipTime(Time.deltaTime);
        }
    }
}