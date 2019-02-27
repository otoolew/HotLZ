using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventOccupantChange : UnityEvent { }

public class Foxhole : MonoBehaviour
{
    [SerializeField] private DefensePosition defensePosition;
    public DefensePosition DefensePosition { get => defensePosition; set => defensePosition = value; }

    [SerializeField] private Soldier currentOccupant;
    public Soldier CurrentOccupant { get => currentOccupant; set => currentOccupant = value; }

    [SerializeField] private CapsuleCollider entryCollider;
    public CapsuleCollider EntryCollider { get => entryCollider; set => entryCollider = value; }

    public EventOccupantChange OccupantChanged;

    private void Start()
    {
        defensePosition = GetComponentInParent<DefensePosition>();
    }
    public void ClaimFoxhole(Soldier soldier)
    {
        currentOccupant = soldier;
        currentOccupant.removed += HandleOccupantDeath;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            if (soldier != null)
                return;
            Debug.Log("Foxhole Hit");
            if (currentOccupant == null)
            {
                ClaimFoxhole(soldier);
                entryCollider.enabled = false;
                OccupantChanged?.Invoke();
            }
            else if(soldier != currentOccupant)
            {               
                soldier.CurrentTerritory.FindFoxhole(soldier);
            }
            
        }
        OccupantChanged?.Invoke();
    }
    public void HandleOccupantDeath(Targetable targetable)
    {
        //currentOccupant.removed -= HandleOccupantDeath;
        //targetable.removed -= HandleOccupantDeath;
        if (targetable == CurrentOccupant)
            currentOccupant.removed -= HandleOccupantDeath;
        currentOccupant = null;
        entryCollider.enabled = true;
        OccupantChanged?.Invoke();
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    Soldier soldier = other.GetComponent<Soldier>();
    //    if (soldier != null && (soldier == CurrentOccupant))
    //    {
    //        CurrentOccupant = null;
    //        OccupantChanged?.Invoke();
    //    }
    //}

}
