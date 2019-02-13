using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Raycast Weapon")]
public class RaycastWeaponSchematic : WeaponSchematic
{
    public int weaponDamage;
    public float weaponRange;

    //private RaycastWeaponController weaponController;

    //public override void Initialize(GameObject obj)
    //{
    //    RaycastWeaponComponent weaponController = obj.GetComponent<RaycastWeaponComponent>();
    //    weaponController.InitComponent();

    //    weaponController.WeaponDamage = weaponDamage;
    //    weaponController.WeaponRange = weaponRange;

    //}

    //public override void TriggerWeaponFire(WeaponComponent weaponController)
    //{
    //    weaponController.Fire();
    //}
}
