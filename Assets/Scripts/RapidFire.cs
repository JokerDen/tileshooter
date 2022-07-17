using UnityEngine;

public class RapidFire : MonoBehaviour
{
    public float modificator;
    private Collectable collectable;
    
    private void Start()
    {
        collectable = GetComponent<Collectable>();
        collectable.onCollect.AddListener(HandleCollect);
    }

    private void HandleCollect(Collector arg0)
    {
        var weapon = arg0.GetComponent<MainWeapon>();
        weapon.weapon.interval *= modificator;
    }
}
