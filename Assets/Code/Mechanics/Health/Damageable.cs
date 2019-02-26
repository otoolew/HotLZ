using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damageable : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField] private float maxHP;
    public float MaxHP { get => maxHP; set => maxHP = value; }

    [SerializeField] private float currentHP;
    public float CurrentHP { get => currentHP; set => currentHP = value; }

    [SerializeField] private bool isDead;
    public bool IsDead { get => isDead; set => isDead = value; }
    #endregion

    public event Action<Damageable> dead;

    #region Monobehaviour
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {

    }

    public virtual void ApplyDamage(float damageAmount)
    {
        currentHP -= damageAmount;
        if(currentHP <= 0)
        {
            isDead = true;
            dead.Invoke(this);
        }
    }
    #endregion
}
