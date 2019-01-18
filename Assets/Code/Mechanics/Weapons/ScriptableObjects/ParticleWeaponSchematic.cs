using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newParticleWeaponSchematic", menuName = "Weapons/Particle Weapon Schematic")]
public class ParticleWeaponSchematic : WeaponSchematic
{
    public override void InitComponent(WeaponComponent weaponComponent)
    {
        weaponComponent.WeaponDamage = WeaponDamage;
        weaponComponent.WeaponRange = WeaponRange;
        weaponComponent.WeaponCooldown = WeaponCooldown;
        weaponComponent.WeaponTimer = WeaponCooldown;
        weaponComponent.WeaponReady = false;
    }

    public override void Cooldown(WeaponComponent weaponComponent)
    {
        throw new System.NotImplementedException();
    }

    public override void Fire(WeaponComponent weaponComponent)
    {
        throw new System.NotImplementedException();
    }
}
