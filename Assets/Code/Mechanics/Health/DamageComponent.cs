using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private Targetable targetable;
    public Targetable Targetable { get => targetable; set => targetable = value; }

    [SerializeField] private float damageMultiplier;
    public float DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
}
