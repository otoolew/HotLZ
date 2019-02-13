using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefensePosition : MonoBehaviour
{
    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private TowerType towerType;
    public TowerType TowerType { get => towerType; set => towerType = value; }

    [SerializeField] private Transform defenderPositioning;
    public Transform DefenderPositioning { get => defenderPositioning; set => defenderPositioning = value; }

    [SerializeField] private Actor currentOccupant;
    public Actor CurrentOccupant { get => currentOccupant; set => currentOccupant = value; }

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            Soldier unit = other.GetComponentInParent<Soldier>();
            if (unit == null)
                return;
            if (currentOccupant == null)
            {
                Debug.Log(unit.name + " is defending!");
                TakeClaim(unit.GetComponent<Actor>());
                currentOccupant.OnActorRemoved += OnOccupantRemoved;
            }
            if (unit.GetComponent<UnitActor>() != currentOccupant)
            {
                territory.FindDefensePosition(unit.GetComponent<UnitActor>());
            }

        }

        TowerCrate towerCrate = other.GetComponent<TowerCrate>();
        if (towerCrate == null)
            return;
        ChangeTowerType(towerCrate);    
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
    public void TakeClaim(Actor defender)
    {
        currentOccupant = defender;
        territory.UpdateFactionOwnership();
    }

    public void OnOccupantRemoved(Actor occupant)
    {
        occupant.OnActorRemoved -= OnOccupantRemoved;
        if (CurrentOccupant != null && occupant.Equals(CurrentOccupant))
        {

            currentOccupant = null;
        }
        territory.UpdateFactionOwnership();
    }
    public void ChangeTowerType(TowerCrate towerCrate)
    {
        TowerType = towerCrate.TowerCrateType;
    }
}
