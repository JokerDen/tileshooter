using UnityEngine;

public class SimpleShooter : MonoBehaviour
{
    public InputPressable input;
    
    public Weapon weapon;
    
    void Update()
    {
        if (input.IsDown())
            weapon.StartShooting();
        if (input.IsUp())
            weapon.StopShooting();
    }
}
