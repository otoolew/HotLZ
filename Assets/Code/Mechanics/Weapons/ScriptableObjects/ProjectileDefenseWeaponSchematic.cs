using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapons/Projectile Defense Weapon")]
public class ProjectileDefenseWeaponSchematic : WeaponSchematic
{
    public GameObject munitionPrefab;

    public float munitionLifeTime;

    public override void Initialize(WeaponComponent weaponComponent)
    {
        ProjectileDefenseWeaponComponent projectileWeapon = weaponComponent.GetComponent<ProjectileDefenseWeaponComponent>();
        projectileWeapon.WeaponTimer = cooldownTime;
    }

    public override void CooldownWeapon(WeaponComponent weaponComponent)
    {
        ProjectileDefenseWeaponComponent projectileWeapon = weaponComponent.GetComponent<ProjectileDefenseWeaponComponent>();
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
        ProjectileDefenseWeaponComponent projectileWeapon = weaponComponent.GetComponent<ProjectileDefenseWeaponComponent>();
        projectileWeapon.Fire(); // TODO: Instantiate and launch from component arg
    }

}
