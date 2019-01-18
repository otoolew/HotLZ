using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent: MonoBehaviour
{
    public abstract int WeaponDamage { get; set; }
    public abstract float WeaponRange { get; set; }
    public abstract float WeaponCooldown { get; set; }
    public abstract float WeaponTimer { get; set; }
    public abstract bool WeaponReady { get; set; }
    #region Properties and Variables

    #endregion
    public abstract void CooldownWeapon();

    public abstract void FireWeapon();

}

