using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefensePosition : MonoBehaviour
{
    [SerializeField] private DefensePositionType defensePositionType;
    public DefensePositionType DefensePositionType { get => defensePositionType; set => defensePositionType = value; }

    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private int currentDefenseIndex;
    public int CurrentDefenseIndex { get => currentDefenseIndex; set => currentDefenseIndex = value; }

    [SerializeField] private DefensePositionWeapon[] defenseWeapons;
    public DefensePositionWeapon[] DefenseWeapons { get => defenseWeapons; }

    [SerializeField] private bool isOccupied;
    public bool IsOccupied { get => isOccupied; set => isOccupied = value; }
    
    #region Events and Handlers

    public void HandleDeath()
    {
        isOccupied = false;

        defenseWeapons[currentDefenseIndex].gameObject.SetActive(false);
        defenseWeapons[currentDefenseIndex].HealthComponent.totalHealthPoints = defenseWeapons[currentDefenseIndex].HealthComponent.maxHealthPoints;
        territory.UpdateFactionOwnership();
    }

    #endregion

    private void Start()
    {
        foreach (var defenseWeapon in defenseWeapons)
        {
            defenseWeapon.GetComponent<HealthComponent>().OnDeath.AddListener(HandleDeath);
            defenseWeapon.gameObject.SetActive(false);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        Soldier soldier = other.GetComponentInParent<Soldier>();
        if (soldier == null)
            return;
        if (!isOccupied)
        {
            Debug.Log(soldier.name + " is defending!");
            isOccupied = true;
            faction = soldier.Faction;
            soldier.Removed();
            ActivateTowerType(currentDefenseIndex);
        }
        else
        {
            territory.FindDefensePosition(soldier.GetComponent<UnitActor>());
        }
 
        if (other.GetComponent<TowerCrate>() != null)
            ChangeTowerType(other.GetComponent<TowerCrate>());
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
    //    {
    //        UnitActor occupant = other.GetComponentInParent<UnitActor>();
    //        OnOccupantRemoved(occupant);
    //    }
    //    //if (currentOccupant != null)
    //    //{
    //    //   OnOccupantRemoved(occupant);
    //    //}
    //}
    public void TakeClaim(Soldier soldier)
    {
        territory.UpdateFactionOwnership();
    }

    public void ChangeTowerType(TowerCrate towerCrate)
    {
        defensePositionType = towerCrate.TowerCrateType;
        if(defensePositionType == DefensePositionType.RIFLE)
        {
            ChangeTowerType(currentDefenseIndex, 0);
        }
        if (defensePositionType == DefensePositionType.ROCKET)
        {
            ChangeTowerType(currentDefenseIndex, 1);
        }
        if (defensePositionType == DefensePositionType.MEDIC)
        {

        }
    }
    /// <summary>
    /// Change Active Tower Type. NOTE: This is Index Out of Bounds Error Prone. 
    /// </summary>
    /// <param name="currentWeaponIndex"></param>
    /// <param name="newWeaponIndex"></param>
    public void ChangeTowerType(int currentWeaponIndex, int newWeaponIndex)
    {
        defenseWeapons[currentDefenseIndex].gameObject.SetActive(false);
        defenseWeapons[newWeaponIndex].gameObject.SetActive(true);
    }

    public void ActivateTowerType(int currentWeaponIndex)
    {
        defenseWeapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void DeactivateAllTowerTypes()
    {
        foreach (var defenseWeapon in defenseWeapons)
        {
            defenseWeapon.gameObject.SetActive(false);
        }
    }
    IEnumerator DeathSequence()
    {
        //TODO: Play Destruction Sequence

        yield return new WaitForSeconds(2.5f);
    }
}
