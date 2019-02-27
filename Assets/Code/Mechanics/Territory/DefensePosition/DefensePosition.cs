using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefensePosition : MonoBehaviour
{
    #region Fields and Properties

    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private FactionComponent factionComponent;
    public FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private TowerTurret towerTurret;
    public TowerTurret TowerTurret { get => towerTurret; set => towerTurret = value; }

    [SerializeField] private int towerPowerLevel;
    public int TowerPowerLevel { get => towerPowerLevel; set => towerPowerLevel = value; }

    public Foxhole[] foxholes;

    #endregion

    #region Events and Handlers

    #endregion

    private void Start()
    {
        factionComponent.GetComponent<FactionComponent>();
        factionComponent.FactionAlignment = FactionAlignment;

        //for (int i = 0; i < foxholes.Length; i++)
        //{
        //    foxholes[i].OccupantChanged.AddListener(UpdateDefensePosition); // TODO: Sloppy handling of Occupant Swap
        //}   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            if (soldier == null)
                return;
            Debug.Log("Defense Hit");
            soldier.ClosestDefensePosition = this;
        }
    }

    public void UpdateDefensePosition()
    {
        int powerLevelResult = 0;

        for (int i = 0; i < foxholes.Length; i++)
        {

            if (foxholes[i].CurrentOccupant != null)
            {
                factionAlignment = foxholes[i].CurrentOccupant.FactionAlignment;
                powerLevelResult++;
                if (foxholes[i].CurrentOccupant.IsDead)
                    foxholes[i].CurrentOccupant = null;
            }           
        }
        towerPowerLevel = powerLevelResult;

        if(towerPowerLevel <= 0)
        {
            factionAlignment = FactionManager.Instance.FactionProvider.NeutralFaction;
        }
        factionComponent.ChangeFactionAlignment(factionAlignment);
    }

    /// <summary>
    /// Change Active Tower Type. NOTE: This is Index Out of Bounds Error Prone. 
    /// </summary>
    /// <param name="currentWeaponIndex"></param>
    /// <param name="newWeaponIndex"></param>
    public void ChangeTowerType(int currentWeaponIndex, int newWeaponIndex)
    {
        //defenseWeapons[currentDefenseIndex].gameObject.SetActive(false);
        //defenseWeapons[newWeaponIndex].gameObject.SetActive(true);
    }

    public void ActivateTowerType(int currentWeaponIndex)
    {
        //defenseWeapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void DeactivateAllTowerTypes()
    {
        //foreach (var defenseWeapon in defenseWeapons)
        //{
        //    defenseWeapon.gameObject.SetActive(false);
        //}
    }
    IEnumerator DeathSequence()
    {
        //TODO: Play Destruction Sequence

        yield return new WaitForSeconds(2.5f);
    }
}
