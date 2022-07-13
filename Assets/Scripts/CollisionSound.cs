using UnityEngine;

public class CollisionSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public float maxForce;
    public float minVolumeThreshold;

    private void OnCollisionEnter(Collision collision)
    {
        Play(Mathf.Clamp01(collision.relativeVelocity.magnitude / maxForce));
    }

    public void Play(float volume = 1f)
    {
        if (volume < minVolumeThreshold) return;
        
        source.PlayOneShot(clip, volume);
    }
}
