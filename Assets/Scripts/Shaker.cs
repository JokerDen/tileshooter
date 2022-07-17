using UnityEngine;

public class Shaker : MonoBehaviour
{
    public float strength;

    private void Start()
    {
        var targets = FindObjectsOfType<Shakeable>();
        foreach (var target in targets)
        {
            target.AddShake(strength);
        }
    }
}