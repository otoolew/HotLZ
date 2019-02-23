using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePositionWeapon : MonoBehaviour
{
    public DefensePosition parentDefensePosition;
    public GameObject mountedSoldier;

    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private TargettingComponent targettingComponent;
    public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private HealthComponent healthComponent;
    public HealthComponent HealthComponent { get => healthComponent; set => healthComponent = value; }

    public event Action<Targetable> targetRemoved;

    private void OnEnable()
    {
        parentDefensePosition = GetComponentInParent<DefensePosition>();
        //parentDefensePosition.Faction.uniform.ChangeUniform(mountedSoldier);
       // targettingComponent.Faction = parentDefensePosition.Faction;
        targettingComponent.ResetTargetter();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthComponent.OnDeath.AddListener(HandlePositionDestroyed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandlePositionDestroyed()
    {
        faction = FactionManager.Instance.FactionProvider.NeutralFaction;
        targettingComponent.ResetTargetter();
        //targetRemoved?.Invoke(this);
    }
}
