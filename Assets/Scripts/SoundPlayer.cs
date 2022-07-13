using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource source;

    public LookInteraction look;

    private void Start()
    {
        if (look != null)
        {
            look.onLook.AddListener(HandleLook);
        }
    }

    private void HandleLook()
    {
        source.Stop();
    }
}
