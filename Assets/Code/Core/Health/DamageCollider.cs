using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class DamageCollider : DamageZone
{
    /// <summary>
    /// On collision enter, see if the colliding object has a Damager and then make the damageableBehaviour take damage
    /// </summary>
    /// <param name="c">The collider</param>
    protected void OnCollisionEnter(Collision c)
    {
        Debug.Log("Collider Hit");
        var damager = c.gameObject.GetComponent<Damager>();

        if (damager == null)
        {
            return;
        }
        LazyLoad();

        float scaledDamage = ScaleDamage(damager.damage);
        damageableBehaviour.TakeDamage(scaledDamage, damager.alignmentProvider);
    }
}
