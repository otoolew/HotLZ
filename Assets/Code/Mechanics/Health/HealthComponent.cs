using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    public Damageable damageable;
    public int maxHealthPoints;
    public int totalHealthPoints;

    public UnityEvent OnDeath;
    public void Start()
    {
        totalHealthPoints = maxHealthPoints;

        if (OnDeath == null)
            OnDeath = new UnityEvent();
    }
    // Start is called before the first frame update
    public void ApplyDamage(int amount)
    {
        totalHealthPoints -= amount;
        if (totalHealthPoints <= 0)
            OnDeath.Invoke();
    }
}
