using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Damage trigger - a trigger based implementation of Damage zone
/// </summary>
[RequireComponent(typeof(Collider))]
public class DamageTriggerCollider : DamageZone
{
    /// <summary>
    /// On entering the trigger see that the collider has a Damager component and if so make the damageableBehaviour take damage
    /// </summary>
    /// <param name="triggeredCollider">The collider that entered the trigger</param>
    protected void OnTriggerEnter(Collider triggeredCollider)
    {
        var damager = triggeredCollider.GetComponent<Damager>();
        if (damager == null)
        {
            return;
        }
        LazyLoad();

        float scaledDamage = ScaleDamage(damager.damage);
        Vector3 collisionPosition = triggeredCollider.ClosestPoint(damager.transform.position);
        damageableBehaviour.TakeDamage(scaledDamage, damager.alignmentProvider);
    }
}
