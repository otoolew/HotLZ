using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    public abstract void InitComponent();
    public abstract void CooldownWeapon();
    public abstract void Fire();
}
