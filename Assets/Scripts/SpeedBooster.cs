using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    public float speed = 20f;
    public float duration = 6f;

    public ParticleSystem rebindEffect;  // TODO: not self destruct now? move to interactor transform, auto-destract particles? 

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        
        var movement = other.GetComponent<MovementController>();
        if (movement == null) return;

        movement.AddBoost(speed, duration);

        Destroy(gameObject);
    }
}