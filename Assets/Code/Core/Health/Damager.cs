using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    /// <summary>
    /// The amount of damage this damager does
    /// </summary>
    public float damage;

    /// <summary>
    /// The event that fires off when the damager has been damaged
    /// </summary>
    public Action hasDamaged;

    /// <summary>
    /// Random chance to spawn collision projectile prefab
    /// </summary>
    [Range(0, 1)]
    public float chanceToSpawnCollisionPrefab = 1.0f;

    /// <summary>
    /// The particle system to fire off when the damager attacks
    /// </summary>
    public ParticleSystem collisionParticles;

    /// <summary>
    /// The alignment of the damager
    /// </summary>
    public SerializableIFactionProvider alignment;

    /// <summary>
    /// Gets the alignment of the damager
    /// </summary>
    public IFactionProvider alignmentProvider
    {
        get { return alignment != null ? alignment.GetInterface() : null; }
    }

    /// <summary>
    /// The logic to set the value of the damage
    /// </summary>
    /// <param name="damageAmount">
    /// The amount to set the damage by, 
    /// will not be set for values less than zero
    /// </param>
    public void SetDamage(float damageAmount)
    {
        if (damageAmount < 0)
        {
            return;
        }
        damage = damageAmount;
    }

    /// <summary>
    /// Damagable will tell damager that it has successful hurt the damagable
    /// </summary>
    public void HasDamaged(IFactionProvider otherAlignment)
    {
        if (hasDamaged != null)
        {
            hasDamaged();
        }
    }

    /// <summary>
    /// Instantiate particle system and play it
    /// </summary>
    void OnCollisionEnter(Collision other)
    {
        if (collisionParticles == null || UnityEngine.Random.value > chanceToSpawnCollisionPrefab)
        {
            return;
        }
        Debug.Log("Try Pool Here");
        //var pfx = Poolable.TryGetPoolable<ParticleSystem>(collisionParticles.gameObject);

        //pfx.transform.position = transform.position;
        //pfx.Play();
    }
}
