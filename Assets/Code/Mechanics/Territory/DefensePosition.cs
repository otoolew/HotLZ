using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Enums;

[Serializable] public class EventDefensePositionTaken : UnityEvent<DefensePosition> { }
[Serializable] public class EventDefensePositionReleased: UnityEvent<DefensePosition> { }

public class DefensePosition : MonoBehaviour
{
    [SerializeField] private DefensePositionType defensePositionType;
    public DefensePositionType DefensePositionType { get => defensePositionType; set => defensePositionType = value; }

    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    //[SerializeField] private int currentWeaponIndex;
    //public int CurrentWeaponIndex { get => currentWeaponIndex; set => currentWeaponIndex = value; }

    //[SerializeField] private DefensePositionWeapon[] defenseWeapons;
    //public DefensePositionWeapon[] DefenseWeapons { get => defenseWeapons; }

    [SerializeField] private Soldier currentOccupant;
    public Soldier CurrentOccupant { get => currentOccupant; set => currentOccupant = value; }

    [SerializeField] private bool isOccupied;
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }

    #region Events and Handlers
    public EventDefensePositionTaken OnDefensePositionTaken;
    public EventDefensePositionTaken OnDefensePositionReleased;

    public void HandleDeath()
    {
        isOccupied = false;
        //defenseWeapons[currentWeaponIndex].gameObject.SetActive(false);
        //defenseWeapons[currentWeaponIndex].HealthComponent.totalHealthPoints = defenseWeapons[currentWeaponIndex].HealthComponent.maxHealthPoints;
        OnDefensePositionReleased.Invoke(this);
    }
    public void HandleOccupantDeath(Soldier soldier)
    {
        Debug.Log("Occupant Death");
        //soldier.targetRemoved -= HandleOccupantDeath;
        isOccupied = false;
        currentOccupant = null;
        faction = FactionManager.Instance.FactionProvider.NeutralFaction;
    }
    #endregion

    private void Start()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {

        Soldier soldier = other.GetComponentInParent<Soldier>();
        if (soldier == null)
            return;
        if (!isOccupied)
        {
            currentOccupant = soldier;
            //currentOccupant.targetRemoved += HandleOccupantDeath;
            Debug.Log(soldier.name + " is defending!");
            isOccupied = true;
            faction = soldier.Faction;
            //ActivateTowerType(currentWeaponIndex);
            OnDefensePositionTaken.Invoke(this);
        }
        else
        {
            territory.FindDefensePosition(soldier.GetComponent<Soldier>());
        }
 
        if (other.GetComponent<TowerCrate>() != null)
            ChangeTowerType(other.GetComponent<TowerCrate>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            if (other.gameObject.GetComponentInParent<Soldier>() == currentOccupant)
            {
                HandleOccupantDeath(currentOccupant);
            }
            Debug.Log("On Trigger Exit");
        }
    }

    // TODO: Fix this 
    public void OnOccupantRemoved(Soldier occupant)
    {
        currentOccupant = null;
    }
    public void ChangeTowerType(TowerCrate towerCrate)
    {
        defensePositionType = towerCrate.TowerCrateType;
        if(defensePositionType == DefensePositionType.RIFLE)
        {
            //ChangeTowerType(currentWeaponIndex, 0);
        }
        if (defensePositionType == DefensePositionType.ROCKET)
        {
            //ChangeTowerType(currentWeaponIndex, 1);
        }
        if (defensePositionType == DefensePositionType.MEDIC)
        {

        }
    }
    ///// <summary>
    ///// Change Active Tower Type. NOTE: This is Index Out of Bounds Error Prone. 
    ///// </summary>
    ///// <param name="currentWeaponIndex"></param>
    ///// <param name="newWeaponIndex"></param>
    //public void ChangeTowerType(int currentWeaponIndex, int newWeaponIndex)
    //{
    //    defenseWeapons[this.currentWeaponIndex].gameObject.SetActive(false);
    //    defenseWeapons[newWeaponIndex].gameObject.SetActive(true);
    //}

    //public void ActivateTowerType(int currentWeaponIndex)
    //{
    //    defenseWeapons[currentWeaponIndex].gameObject.SetActive(true);
    //}

    //public void DeactivateAllTowerTypes()
    //{
    //    foreach (var defenseWeapon in defenseWeapons)
    //    {
    //        defenseWeapon.gameObject.SetActive(false);
    //    }
    //}
    IEnumerator DeathSequence()
    {
        //TODO: Play Destruction Sequence

        yield return new WaitForSeconds(2.5f);
    }
}
