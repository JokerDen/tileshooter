using UnityEngine;

public class CollectableWeapon : MonoBehaviour
{
    public Weapon weapon;
    
    void Start()
    {
        GetComponent<Collectable>().onCollect.AddListener(Collect);
    }

    private void Collect(Collector arg0)
    {
        var weapon = arg0.GetComponent<MainWeapon>();
        weapon.Apply(this.weapon);
    }
}
