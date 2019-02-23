using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableBehaviour : MonoBehaviour
{
    /// <summary>
    /// The alignment of the damager
    /// </summary>
    public SerializableIFactionProvider alignment;
    /// <summary>
    /// Gets the alignment of the damageable
    /// </summary>
    public IFactionProvider FactionProvider => alignment?.GetInterface();

    [SerializeField] private float maxHealth;
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    [SerializeField] private float currentHealth;
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }

    [SerializeField] private bool isDead;
    public bool IsDead { get => isDead; set => isDead = value; }
    /// <summary>
    /// Returns our targetable's transform position
    /// </summary>
    public virtual Vector3 position
    {
        get { return transform.position; }
    }
    /// <summary>
    /// Event that is fired when this instance is removed, such as when pooled or destroyed
    /// </summary>
    public event Action<DamageableBehaviour> removed;

    /// <summary>
    /// Event that is fired when this instance is killed
    /// </summary>
    public event Action<DamageableBehaviour> died;

    /// <summary>
    /// Takes the damage and also provides a position for the damage being dealt
    /// </summary>
    /// <param name="damageValue">Damage value.</param>
    /// <param name="alignment">Alignment value</param>
    public virtual void TakeDamage(float damageValue, IFactionProvider alignment)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
            Death();

    }
    protected virtual void Awake()
    {

    }

    /// <summary>
    /// Removes this damageable without killing it
    /// </summary>
    public virtual void Remove()
    {
        // Set health to zero so that this behaviour appears to be dead. This will not fire death events
        currentHealth = 0;
        OnRemoved();
    }

    /// <summary>
    /// Fires kill events
    /// </summary>
    void Death()
    {
        died?.Invoke(this);
        Remove();
        GetComponent<Animator>().SetBool("IsDead", true);
    }

    /// <summary>
    /// Fires the removed event
    /// </summary>
    void OnRemoved()
    {
        removed?.Invoke(this);
    }

}
