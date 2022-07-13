using UnityEngine;

public class Burstable : MonoBehaviour
{
    private ParticleSystem ps;

    public void Play()
    {
        if (ps == null)
            ps = GetComponent<ParticleSystem>();
        
        ps.Play();
    }
}
