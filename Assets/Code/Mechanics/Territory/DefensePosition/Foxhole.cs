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
        soldier.HealthComponent.OnDeath.AddListener(HandleOccupantDeath);
        currentOccupant = soldier;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            if ((soldier != null) && (soldier == currentOccupant))
            {
                entryCollider.enabled = false;
                OccupantChanged?.Invoke();
            }          
        }       
    }
    public void HandleOccupantDeath()
    {
        currentOccupant.HealthComponent.OnDeath.RemoveListener(HandleOccupantDeath);
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
