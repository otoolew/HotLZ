using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newWeapon", menuName = "Weapon")]
public class WeaponSchematic : ScriptableObject
{
    public int WeaponDamage;
    public float WeaponRange;
    public float WeaponCooldown;

}
