using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Projectile Weapon")]
public class ProjectileWeaponSchematic : WeaponSchematic
{
    public GameObject munitionPrefab;

    public float munitionLifeTime;

    public override void Initialize(WeaponComponent weaponComponent)
    {
        ProjectileWeaponComponent projectileWeapon = weaponComponent.GetComponent<ProjectileWeaponComponent>();
        projectileWeapon.WeaponTimer = cooldownTime;
    }

    public override void CooldownWeapon(WeaponComponent weaponComponent)
    {
        ProjectileWeaponComponent projectileWeapon = weaponComponent.GetComponent<ProjectileWeaponComponent>();
        if (projectileWeapon.WeaponTimer <= 0)
        {
            projectileWeapon.WeaponTimer = 0;
            projectileWeapon.WeaponReady = true;
        }
        else
        {
            projectileWeapon.WeaponTimer -= Time.deltaTime;
            projectileWeapon.WeaponReady = false;
        }
    }

    public override void TriggerWeaponFire(WeaponComponent weaponComponent)
    {
        ProjectileWeaponComponent projectileWeapon = weaponComponent.GetComponent<ProjectileWeaponComponent>();
        projectileWeapon.Fire();
    }
}
