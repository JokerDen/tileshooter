using System;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public InputPressable cycleWeapon;

    public Weapon[] weapons;

    private int currentIndex;

    public SimpleShooter shooter;

    private void Start()
    {
        UpdateCurrentWeapon();
    }

    private void UpdateCurrentWeapon()
    {
        foreach (var weapon in weapons)
        {
            weapon.SetShooting(false);
            weapon.gameObject.SetActive(false);
        }
            
        weapons[currentIndex].gameObject.SetActive(true);
        shooter.weapon = weapons[currentIndex];
    }

    private void Update()
    {
        if (cycleWeapon.IsDown())
        {
            currentIndex++;
            if (currentIndex >= weapons.Length)
                currentIndex = 0;

            UpdateCurrentWeapon();
        }
    }
}
