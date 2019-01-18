using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResourceField : MonoBehaviour
{
    #region Fields and Properties

    [Header("Owning Faction")]
    [SerializeField]
    private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    public Transform[] navPointArray;

    [SerializeField]
    private int totalResources;
    public int TotalResources { get => totalResources; set => totalResources = value; }

    [SerializeField]
    private int currentResourceAmount;
    public int CurrentResourceAmount { get => currentResourceAmount; set => currentResourceAmount = value; }

    [SerializeField]
    private int collectorCount;
    public int CollectorCount { get => collectorCount; set => collectorCount = value; }

    #endregion

    #region Events


    #endregion
    #region Debug
    //public Text textLabel;
    //public Text resourceLabel;
    //public Text factionLabel;
    #endregion


    #region Monobehaviour
    private void Awake()
    {

    }
    private void Start()
    {
        currentResourceAmount = totalResources;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        SupplyTruckDriver supplyTruck = other.GetComponentInParent<SupplyTruckDriver>();
        if (supplyTruck == null)
            return;
        supplyTruck.LoadResources();
        currentResourceAmount--;
    }
    #endregion


    //void OnDefenderDeath(Starship defender)
    //{
    //    defender.removed -= OnDefenderDeath;
    //    for (int i = 0; i < defendersInRange.Count; i++)
    //    {
    //        if (defendersInRange[i] == defender)
    //        {
    //            defendersInRange.RemoveAt(i);
    //            if (defender.GetComponent<Faction>().IsAlly(FactionAlignment))
    //                defenderCount--;
    //            else
    //                attackerCount--;
    //            break;
    //        }
    //    }
    //}
}