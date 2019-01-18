using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    public KeyCodeVariable debugSpawnKey;

    [SerializeField]
    private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField]
    private int riflemenPoolCount;
    public int RiflemenPoolCount { get => riflemenPoolCount; set => riflemenPoolCount = value; }

    [SerializeField]
    private UnitActorSchematic soldierAsset;
    public UnitActorSchematic SoldierAsset { get => soldierAsset; set => soldierAsset = value; }

    [SerializeField]
    private Transform soldierSpawnPoint;
    public Transform SoliderSpawnPoint { get => soldierSpawnPoint; set => soldierSpawnPoint = value; }

    [SerializeField]
    private Transform soldierReturnPoint;
    public Transform SoldierReturnPoint { get => soldierReturnPoint; set => soldierReturnPoint = value; }

    public List<UnitActor> soldierList = new List<UnitActor>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < riflemenPoolCount; i++)
        {

            UnitActor newUnit = UnitActorFactory.InstantiatePrefab(soldierAsset);
            newUnit.Pooled = true;
            //newUnit.Faction = faction;
            newUnit.name = faction.name + " Soldier";
            newUnit.transform.parent = null;
            newUnit.gameObject.SetActive(false);
            soldierList.Add(newUnit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debugSpawnKey.KeyDownValue())
            SpawnRifleman();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            Soldier unit = other.GetComponentInParent<Soldier>();
            if (unit == null)
                return;
            unit.GetComponent<UnitActor>().Pooled = true;
            unit.GetComponent<UnitActor>().Dead = false;
            unit.GetComponent<HealthController>().totalHealthPoints = 100;
            unit.gameObject.SetActive(false);
        }
    }

    public void SpawnRifleman()
    {
        foreach (var infantry in soldierList)
        {
            if (!infantry.isActiveAndEnabled && infantry.Pooled)
            {
                infantry.Pooled = false;
                infantry.transform.position = soldierSpawnPoint.position;
                infantry.gameObject.SetActive(true);
                return;
            }
        }
    }

}
