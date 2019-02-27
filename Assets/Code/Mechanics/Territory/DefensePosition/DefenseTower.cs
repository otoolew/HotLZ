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

    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private TowerTurret currentTowerTurret;
    public TowerTurret CurrentTowerTurret { get => currentTowerTurret; set => currentTowerTurret = value; }

    public TowerTurret[] towerTurrets;

    // Start is called before the first frame update
    void Start()
    {
        currentTowerTurret = towerTurrets[0];
        //currentTowerTurret.targetRemoved += HandleTowerDestruction;
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
