using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefensePosition : MonoBehaviour
{
    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private DefensePositionType defensePositionType;
    public DefensePositionType DefensePositionType { get => defensePositionType; set => defensePositionType = value; }

    [SerializeField] private DefensePositionWeapon[] defensePositionWeapons;
    public DefensePositionWeapon[] DefensePositionWeapons { get => defensePositionWeapons;}

    [SerializeField] private Transform defenderPositioning;
    public Transform DefenderPositioning { get => defenderPositioning; set => defenderPositioning = value; }

    [SerializeField] private Actor currentOccupant;
    public Actor CurrentOccupant { get => currentOccupant; set => currentOccupant = value; }

    public GameObject[] weaponTypeModels;

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
        defensePositionType = towerCrate.TowerCrateType;
        if(defensePositionType == DefensePositionType.RIFLE)
        {
            for (int i = 0; i < DefensePositionWeapons.Length; i++)
            {
                //if(DefensePositionWeapons[i].to)
            }
        }
        if (defensePositionType == DefensePositionType.RIFLE)
        {

        }
        if (defensePositionType == DefensePositionType.RIFLE)
        {

        }
    }
}
