using UnityEngine;

public class MainWeapon : MonoBehaviour
{
    public Weapon weapon;

    public void Apply(Weapon weapon)
    {
        Destroy(this.weapon.gameObject);
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        this.weapon = weapon;
        weapon.gameObject.SetActive(true);
    }
}
