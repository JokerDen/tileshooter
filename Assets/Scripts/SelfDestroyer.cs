using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Invoke("SelfDestroy", lifetime);
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
