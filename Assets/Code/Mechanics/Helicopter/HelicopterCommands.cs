﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterCommands : MonoBehaviour
{
    public KeyCodeVariable offerPickUpKey;
    public KeyCodeVariable debugDropKey;
    public KeyCodeVariable operateCraneKey;

    [SerializeField] private HeadQuarters headQuarters;
    public HeadQuarters HeadQuarters { get => headQuarters; set => headQuarters = value; }

    [SerializeField] private HelicopterCrane helicopterCrane;
    public HelicopterCrane HelicopterCrane { get => helicopterCrane; set => helicopterCrane = value; }

    [SerializeField] private float troopPickCallRadius;
    public float TroopPickCallRadius { get => troopPickCallRadius; set => troopPickCallRadius = value; }

    [SerializeField]
    private int seatCapacity;
    public int SeatCapacity { get => seatCapacity; set => seatCapacity = value; }

    [SerializeField]
    private int seatTotal;
    public int SeatTotal { get => seatTotal; set => seatTotal = value; }

    [SerializeField]
    private Transform unitDropPoint;
    public Transform UnitDropPoint { get => unitDropPoint; set => unitDropPoint = value; }

    [SerializeField]
    private bool grounded;
    public bool Grounded { get => grounded; set => grounded = value; }

    public Stack<Soldier> loadedUnits = new Stack<Soldier>();

    [SerializeField] private LayerMask layerMask;
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debugDropKey.KeyDownValue())
            UnloadUnit();
        if (offerPickUpKey.KeyDownValue())
        {
            OfferPickUp();
        }
        if (operateCraneKey.KeyDownValue())
        {
            if (HelicopterCrane.IsLowered)
            {
                HelicopterCrane.RaiseCrane();
            }
            else
            {
                HelicopterCrane.LowerCrane();
            }
        }
            

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = true;
        }
        Soldier unit = other.GetComponentInParent<Soldier>();
        if (unit == null)
            return;
        if (unit.FactionComponent.Alignment == GetComponentInParent<HelicopterUnit>().FactionComponent.Alignment)
            LoadUnit(unit);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = false;
        }
    }

    public Territory ClosestTerritory()
    {
        if (MapManager.Instance.territories.Length <= 0)
            return null;

        Territory closest = (Territory)MapManager.Instance.territories[0];
        float closestDistance = 0f;
        for (int i = 0; i < MapManager.Instance.territories.Length; i++)
        {
            float distance = Vector3.Distance(closest.transform.position, MapManager.Instance.territories[i].transform.position);
            if (distance < closestDistance)
                closest = (Territory)MapManager.Instance.territories[i];
        }
        return closest;
    }

    public void OfferPickUp()
    {
        if (SeatTotal == SeatCapacity)
            return;
        Collider[] troops = Physics.OverlapSphere(transform.position, troopPickCallRadius, layerMask);
        Debug.Log(troops.Length);
        if (troops.Length > 0)
        {
            for (int i = 0; i < troops.Length; i++)
            {
                Soldier soldier = troops[i].GetComponentInParent<Soldier>();
                if (soldier != null)
                {
                    //if(soldier.ClosestDefensePosition != null)
                    //{
                    //    soldier.ClosestDefensePosition.GetComponent<Collider>().enabled = true;
                    //    soldier.ClosestDefensePosition = null;
                    //}
                    soldier.NavigationAgent.GoToPosition(transform.position);
                }
            }
        }
    }

    public void LoadUnit(Soldier unit)
    {
        if (seatTotal < seatCapacity)
        {
            unit.gameObject.SetActive(false);
            loadedUnits.Push(unit);
            //Debug.Log("Loaded " + unit.name);
        }
        seatTotal = loadedUnits.Count;
    }
    public void UnloadUnit()
    {
        if (!grounded)
            return;
        if (loadedUnits.Count > 0)
        {
            Soldier unit = loadedUnits.Pop();
            unit.transform.position = unitDropPoint.position;
            unit.gameObject.SetActive(true);

            unit.NavigationAgent.GoToPosition(ClosestTerritory().ClosestDefensePosition(transform).transform.position);
        }
        else
        {
            //Debug.Log("Nothing Loaded");
        }
        seatTotal = loadedUnits.Count;
    }
}
