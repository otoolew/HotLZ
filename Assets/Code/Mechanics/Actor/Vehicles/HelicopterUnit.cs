using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterUnit : UnitActor
{
    [SerializeField] private FactionAlignment faction;
    public override FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private Enums.UnitType unitType;
    public override Enums.UnitType UnitType { get => unitType; set => unitType = value; }

    [SerializeField] private HealthController healthController;
    public override HealthController HealthController { get => healthController; set => healthController = value; }

    [SerializeField] private bool dead;
    public override bool Dead { get => dead; set => dead = value; }

    [SerializeField] private bool pooled;
    public override bool Pooled { get => pooled; set => pooled = value; }

    public override event Action<Actor> OnActorRemoved;

    public override void UnitActorDeath()
    {
        dead = true;
        OnActorRemoved?.Invoke(this);
        StartCoroutine("DeathSequence");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2.5f);
        Pooled = true;
        gameObject.SetActive(false);
    }
}
