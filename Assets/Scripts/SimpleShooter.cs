using UnityEngine;

public class SimpleShooter : MonoBehaviour
{
    public InputPressable input;
    
    public Weapon weapon;

    public MainWeapon mainWeapon;
    
    void Update()
    {
        if (weapon != null)
            weapon.SetShooting(input.IsPressing());
        
        if (mainWeapon != null)
            mainWeapon.weapon.SetShooting(input.IsPressing());
    }
}
