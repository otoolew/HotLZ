using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefensePosition : Targetable
{
    [SerializeField] private DefensePositionType defensePositionType;
    public DefensePositionType DefensePositionType { get => defensePositionType; set => defensePositionType = value; }

    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private FactionAlignment faction;
    public override FactionAlignment Faction { get => faction; set => faction = value; }

    //[SerializeField] private DefenseWeapon currentDefenseWeapon;
    //public DefenseWeapon CurrentDefenseWeapon { get => currentDefenseWeapon; }

    //[SerializeField] private DefenseWeapon[] defenseWeapons;
    //public DefenseWeapon[] DefenseWeapons { get => defenseWeapons; }

    [SerializeField] private HealthComponent healthComponent;
    public override HealthComponent HealthComponent { get => healthComponent; set => healthComponent = value; }

    [SerializeField] private bool isOccupied;
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }

    #region Events and Handlers
    public override event Action<Targetable> OnTargetRemoved;

    public void HandleDeath()
    {
        OnTargetRemoved?.Invoke(this);
        StartCoroutine("DeathSequence");
        territory.UpdateFactionOwnership();
    }

    #endregion

    private void Start()
    {
        healthComponent.OnDeath.AddListener(HandleDeath);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            Soldier unit = other.GetComponentInParent<Soldier>();
            if (unit == null)
                return;
            if (!isOccupied)
            {
                Debug.Log(unit.name + " is defending!");
                //unit.
            }
            //if (unit.GetComponent<UnitActor>() != currentOccupant)
            //{
            //    territory.FindDefensePosition(unit.GetComponent<UnitActor>());
            //}
        }
        
        if (other.GetComponent<TowerCrate>() != null)
            ChangeTowerType(other.GetComponent<TowerCrate>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            UnitActor occupant = other.GetComponentInParent<UnitActor>();
            OnOccupantRemoved(occupant);
        }
        //if (currentOccupant != null)
        //{
        //   OnOccupantRemoved(occupant);
        //}
    }
    public void TakeClaim(Soldier soldier)
    {
        territory.UpdateFactionOwnership();
    }

    public void OnOccupantRemoved(UnitActor occupant)
    {
        territory.UpdateFactionOwnership();
    }
    public void ChangeTowerType(TowerCrate towerCrate)
    {
        defensePositionType = towerCrate.TowerCrateType;
        if(defensePositionType == DefensePositionType.RIFLE)
        {
            //for (int i = 0; i < defenseWeapons.Length; i++)
            //{
            //    //if(DefensePositionWeapons[i].to)
            //}
        }
        if (defensePositionType == DefensePositionType.RIFLE)
        {

        }
        if (defensePositionType == DefensePositionType.RIFLE)
        {

        }
    }
    IEnumerator DeathSequence()
    {
        //currentDefenseWeapon = defenseWeapons[0];
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(true);
    }
}
