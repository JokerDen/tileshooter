using UnityEngine;

public class SimpleShooter : MonoBehaviour
{
    public InputPressable input;
    
    public Weapon weapon;
    
    void Update()
    {
        weapon.SetShooting(input.IsPressing());
    }
}
