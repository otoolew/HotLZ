using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Raycast Weapon")]
public class RaycastWeaponSchematic : WeaponSchematic
{
    public int weaponDamage;
    public float weaponRange;

    public override void Initialize(WeaponComponent weaponComponent)
    {
        RaycastWeaponComponent rayComponent = weaponComponent.GetComponent<RaycastWeaponComponent>();
        rayComponent.WeaponDamage = weaponDamage;
        rayComponent.WeaponRange = weaponRange;
        rayComponent.WeaponCooldown = cooldownTime;
    }

    public override void CooldownWeapon(WeaponComponent weaponComponent)
    {
        RaycastWeaponComponent rayComponent = weaponComponent.GetComponent<RaycastWeaponComponent>();
        if (rayComponent.WeaponTimer <= 0)
        {
            rayComponent.WeaponTimer = 0;
            rayComponent.WeaponReady = true;
        }
        else
        {
            rayComponent.WeaponTimer -= Time.deltaTime;
            rayComponent.WeaponReady = false;
        }
    }

    public override void TriggerWeaponFire(WeaponComponent weaponComponent)
    {
        RaycastWeaponComponent rayComponent = weaponComponent.GetComponent<RaycastWeaponComponent>();
        rayComponent.Fire();
    }
}
