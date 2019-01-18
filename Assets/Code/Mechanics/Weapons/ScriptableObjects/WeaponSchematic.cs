using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSchematic : ScriptableObject
{
    public int WeaponDamage;
    public float WeaponRange;
    public float WeaponCooldown;
    public abstract void InitComponent(WeaponComponent weaponComponent);
    public abstract void Cooldown(WeaponComponent weaponComponent);
    public abstract void Fire(WeaponComponent weaponComponent);

}
