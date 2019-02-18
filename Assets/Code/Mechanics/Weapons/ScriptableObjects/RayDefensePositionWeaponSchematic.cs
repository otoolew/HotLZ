using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Raycast Defense Weapon")]
public class RayDefensePositionWeaponSchematic : WeaponSchematic
{
    public int weaponDamage;
    public float weaponRange;

    public override void Initialize(WeaponComponent weaponComponent)
    {
        RayDefensePositionComponent rayWeapon = weaponComponent.GetComponent<RayDefensePositionComponent>();
        rayWeapon.WeaponDamage = weaponDamage;
        rayWeapon.WeaponRange = weaponRange;
        rayWeapon.WeaponCooldown = cooldownTime;
    }

    public override void CooldownWeapon(WeaponComponent weaponComponent)
    {
        RayDefensePositionComponent rayWeapon = weaponComponent.GetComponent<RayDefensePositionComponent>();

        if (rayWeapon.WeaponTimer <= 0)
        {
            rayWeapon.WeaponTimer = 0;
            rayWeapon.WeaponReady = true;
        }
        else
        {
            rayWeapon.WeaponTimer -= Time.deltaTime;
            rayWeapon.WeaponReady = false;
        }
    }

    public override void TriggerWeaponFire(WeaponComponent weaponComponent)
    {
        RayDefensePositionComponent rayWeapon = weaponComponent.GetComponent<RayDefensePositionComponent>();
        rayWeapon.Fire();
    }
}
