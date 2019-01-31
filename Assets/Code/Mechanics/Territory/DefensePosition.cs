using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePosition : MonoBehaviour
{
    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private Enums.SoldierType preferedUnitType;
    public Enums.SoldierType PreferedUnitType { get => preferedUnitType; set => preferedUnitType = value; }

    [SerializeField] private Actor currentOccupant;
    public Actor CurrentOccupant { get => currentOccupant; set => currentOccupant = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            Soldier unit = other.GetComponentInParent<Soldier>();
            if (unit == null)
                return;
            if (currentOccupant == null)
            {
                //Debug.Log(unit.name + " is defending!");
                TakeClaim(unit.GetComponent<Actor>());
                currentOccupant.OnActorRemoved += OnOccupantRemoved;
            }
            if (unit.GetComponent<UnitActor>() != currentOccupant)
            {
                territory.FindDefensePosition(unit.GetComponent<UnitActor>());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        UnitActor occupant = other.GetComponentInParent<UnitActor>();
        OnOccupantRemoved(occupant);
        //if (currentOccupant != null)
        //{
        //   OnOccupantRemoved(occupant);
        //}
    }
    public void TakeClaim(Actor defender)
    {
        currentOccupant = defender;
    }
    public void OnOccupantRemoved(Actor occupant)
    {
        occupant.OnActorRemoved -= OnOccupantRemoved;
        if (CurrentOccupant != null && occupant.Equals(CurrentOccupant))
        {
            currentOccupant = null;
        }

    }

}
