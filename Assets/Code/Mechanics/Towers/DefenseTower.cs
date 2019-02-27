using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class DefenseTower : MonoBehaviour
{
    [SerializeField] private DefenseTowerState defenseTowerState;
    public DefenseTowerState DefenseTowerState { get => defenseTowerState; set => defenseTowerState = value; }

    [SerializeField] private DefenseTowerType defensePositionType;
    public DefenseTowerType DefensePositionType { get => defensePositionType; set => defensePositionType = value; }

    [SerializeField] private DefensePosition parentDefensePosition;
    public DefensePosition ParentDefensePosition { get => parentDefensePosition; set => parentDefensePosition = value; }

    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private TowerTurret currentTowerTurret;
    public TowerTurret CurrentTowerTurret { get => currentTowerTurret; set => currentTowerTurret = value; }

    public TowerTurret[] towerTurrets;

    // Start is called before the first frame update
    void Start()
    {
        parentDefensePosition = GetComponentInParent<DefensePosition>();
        currentTowerTurret = towerTurrets[0];
        parentDefensePosition.FactionComponent.FactionAlignmentChange.AddListener(OnFactionAlignmentChange);
    }

    // TODO: This Method is redundant. Can be handled at the TargettingComponent
    public void OnFactionAlignmentChange(FactionAlignment newFactionAlignment)
    {
        factionAlignment = newFactionAlignment;
    }
    public void ActivateTurret(TowerTurret towerTurret)
    {
        currentTowerTurret = towerTurret;
    }

    public void DeactivateTurret(TowerTurret towerTurret)
    {

    }

    public void HandleTowerDestruction(Targetable tower)
    {
        Debug.Log(tower.name + " Destroyed");
        //tower.gameObject.SetActive(false);
    }

    public void ChangeTowerType(TowerCrate towerCrate)
    {
        defensePositionType = towerCrate.TowerCrateType;
        if (defensePositionType == DefenseTowerType.RIFLE)
        {
            //ChangeTowerType(currentDefenseIndex, 0);
        }
        if (defensePositionType == DefenseTowerType.ROCKET)
        {
            //ChangeTowerType(currentDefenseIndex, 1);
        }
        if (defensePositionType == DefenseTowerType.MEDIC)
        {

        }
    }

}
