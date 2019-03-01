using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefensePosition : MonoBehaviour
{
    #region Fields and Properties

    public Territory ResidingTerritory { get; set; }

    [SerializeField] private FactionComponent factionComponent;
    public FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    [SerializeField] private TowerTurret towerTurret;
    public TowerTurret TowerTurret { get => towerTurret; set => towerTurret = value; }

    [SerializeField] private int towerPowerLevel;
    public int TowerPowerLevel { get => towerPowerLevel; set => towerPowerLevel = value; }

    [SerializeField] private int positionTotal;
    [SerializeField] private int blueTroopCount;
    [SerializeField] private int redTroopCount;

    public PositionAssignment[] positionAssignments;

    #endregion

    #region Events and Handlers

    #endregion

    private void Start()
    {
        factionComponent.GetComponent<FactionComponent>();
        ResidingTerritory = GetComponentInParent<Territory>();
        positionTotal = positionAssignments.Length;

        for (int i = 0; i < positionTotal; i++)
        {
            positionAssignments[i].SoldierChange += OnSoldierChange;
        }       
    }
    public void OnSoldierChange()
    {
        int blueCount = 0;
        int redCount = 0;
        for (int i = 0; i < positionAssignments.Length; i++)
        {
            if(positionAssignments[i].SoldierArrived && positionAssignments[i].AssignedSoldier != null)
            {
                switch (positionAssignments[i].FactionAlignment.factionAlignmentType)
                {
                    case FactionAlignmentType.NEUTRAL:
                        break;
                    case FactionAlignmentType.BLUE:
                        blueCount++;
                        break;
                    case FactionAlignmentType.RED:
                        redCount++;
                        break;
                    default:
                        break;
                }
            }
        }
        blueTroopCount = blueCount;
        redTroopCount = redCount;
        if((blueTroopCount > redTroopCount) && factionComponent.Alignment != FactionManager.Instance.FactionProvider.BlueFaction)
        {
            FactionComponent.ChangeFactionAlignment(FactionManager.Instance.FactionProvider.BlueFaction);
            TowerTurret.FactionComponent.Alignment = FactionManager.Instance.FactionProvider.BlueFaction;
            TowerTurret.ResetTowerTurret();
            return;
        }
        if ((redTroopCount > blueTroopCount) && factionComponent.Alignment != FactionManager.Instance.FactionProvider.RedFaction)
        {
            FactionComponent.ChangeFactionAlignment(FactionManager.Instance.FactionProvider.RedFaction);
            TowerTurret.FactionComponent.Alignment = FactionManager.Instance.FactionProvider.RedFaction;
            TowerTurret.ResetTowerTurret();
            return;
        }
    }
    
    public void UpdateDefensePosition()
    {

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
