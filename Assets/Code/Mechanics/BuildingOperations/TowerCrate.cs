﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enums;

public class TowerCrate : MonoBehaviour
{
    [SerializeField] private Vector3 spawnPoint;
    public Vector3 SpawnPoint { get => spawnPoint; set => spawnPoint = value; }

    [SerializeField] private DefenseTowerType towerCrateType;
    public DefenseTowerType TowerCrateType { get => towerCrateType; set => towerCrateType = value; }

    private void Start()
    {
        spawnPoint = transform.position;
    }
    private void OnEnable()
    {
        
    }
    public void SpawnCrate()
    {
        transform.position = spawnPoint;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        DefenseTower defensePosition = other.GetComponent<DefenseTower>();

        if (defensePosition != null)
        {
            Debug.Log(defensePosition + " current type is " + defensePosition.DefensePositionType.ToString() + ". Changing Type to " + towerCrateType.ToString());
            defensePosition.ChangeTowerType(this);
            
            transform.parent = null;
            gameObject.SetActive(false);
        }
    }
}
