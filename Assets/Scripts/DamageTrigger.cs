using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    public Trigger trig;
    
    void Start()
    {
        GetComponent<Damageable>().onHit.AddListener(Trig);
    }

    private void Trig()
    {
        // Debug.Log("Damage Trigger");
        trig.Trig();
    }
}
