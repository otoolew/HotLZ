using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newProjectileWeaponSchematic", menuName = "Weapons/Projectile Weapon Schematic")]
public class ProjectileWeaponSchematic : WeaponSchematic
{
    public GameObject munitionPrefab;

    public override void InitComponent(WeaponComponent weaponComponent)
    {
        throw new System.NotImplementedException();
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
