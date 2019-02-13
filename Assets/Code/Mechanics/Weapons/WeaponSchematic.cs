using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponSchematic : ScriptableObject
{
    public string weaponName;
    public AudioClip soundEffect;
    public float cooldownTime;

    //public abstract void Initialize(GameObject obj);
    //public abstract void TriggerWeaponFire(WeaponComponent weaponComponent);
}
