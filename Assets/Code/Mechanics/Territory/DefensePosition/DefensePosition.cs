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

    [SerializeField] private DefenseTower defenseTower;
    public DefenseTower DefenseTower { get => defenseTower; set => defenseTower = value; }

    [SerializeField] private int towerPowerLevel;
    public int TowerPowerLevel { get => towerPowerLevel; set => towerPowerLevel = value; }

    public PositionAssignment[] positionAssignments;

    #endregion

    #region Events and Handlers

    #endregion

    private void Start()
    {
        factionComponent.GetComponent<FactionComponent>();
        ResidingTerritory = GetComponentInParent<Territory>();
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
