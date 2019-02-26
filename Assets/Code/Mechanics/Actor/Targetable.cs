using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private float maxHP;
    public float MaxHP { get => maxHP; set => maxHP = value; }

    [SerializeField] private float currentHP;
    public float CurrentHP { get => currentHP; set => currentHP = value; }

    [SerializeField] private bool isDead;
    public bool IsDead { get => isDead; set => isDead = value; }

    #endregion
    public event Action<Targetable> dead;

    public virtual void ApplyDamage(float damageAmount)
    {
        currentHP -= damageAmount;
        if (currentHP <= 0)
        {
            isDead = true;
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        dead.Invoke(this);
    }
    //public UnityEvent OnDeath;
    //public void Start()
    //{
    //    targetable = GetComponent<Targetable>();
    //    totalHealthPoints = maxHealthPoints;

    //    if (OnDeath == null)
    //        OnDeath = new UnityEvent();
    //}
    //// Start is called before the first frame update
    //public void ApplyDamage(int amount)
    //{
    //    totalHealthPoints -= amount;
    //    if (totalHealthPoints <= 0)
    //        OnDeath.Invoke();
    //}
}
