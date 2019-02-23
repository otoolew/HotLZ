using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackComponent : MonoBehaviour
{
    public SerializableIFactionProvider alignment;
    public IFactionProvider FactionProvider => alignment?.GetInterface();
    public abstract float AttackDamage { get; set; }
    public abstract void Cooldown();
    public abstract void FireAttack();

}
