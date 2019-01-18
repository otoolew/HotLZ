using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageComponent : MonoBehaviour
{
    public int damageMultiplier;

    public UnityEvent<int> OnHit;

    public void TakeDamage(int amount)
    {
        int totalDamage = amount * damageMultiplier;
        OnHit.Invoke(totalDamage);
    }
}
