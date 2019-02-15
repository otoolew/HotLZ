using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Soldier Weapon")]
public class SoldierWeaponSchematic : WeaponSchematic
{
    public int weaponDamage;
    public float weaponRange;
    public override void CooldownWeapon(WeaponComponent weaponComponent)
    {
        SoldierWeaponComponent soldierWeapon = weaponComponent.GetComponent<SoldierWeaponComponent>();
        soldierWeapon.WeaponDamage = weaponDamage;
        soldierWeapon.WeaponRange = weaponRange;
        soldierWeapon.WeaponCooldown = cooldownTime;
    }

    public override void Initialize(WeaponComponent weaponComponent)
    {
        SoldierWeaponComponent soldierWeapon = weaponComponent.GetComponent<SoldierWeaponComponent>();
        if (soldierWeapon.WeaponTimer <= 0)
        {
            soldierWeapon.WeaponTimer = 0;
            soldierWeapon.WeaponReady = true;
        }
        else
        {
            soldierWeapon.WeaponTimer -= Time.deltaTime;
            soldierWeapon.WeaponReady = false;
        }
    }

    public override void TriggerWeaponFire(WeaponComponent weaponComponent)
    {
        SoldierWeaponComponent soldierWeapon = weaponComponent.GetComponent<SoldierWeaponComponent>();
        soldierWeapon.Fire();
    }
}
