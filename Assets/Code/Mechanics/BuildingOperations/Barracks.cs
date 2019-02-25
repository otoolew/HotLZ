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

    [SerializeField]
    private RallyPoint baseRallyPoint;
    public RallyPoint BaseRallyPoint { get => baseRallyPoint; set => baseRallyPoint = value; }

    [SerializeField]
    private float spawnCooldown;
    public float SpawnCooldown { get => spawnCooldown; set => spawnCooldown = value; }

    [SerializeField]
    private float spawnTimer;
    public float SpawnTimer { get => spawnTimer; set => spawnTimer = value; }

    [SerializeField]
    private bool spawnReady;
    public bool SpawnReady { get => spawnReady; set => spawnReady = value; }

    public List<UnitActor> soldierList = new List<UnitActor>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < riflemenPoolCount; i++)
        {
            GameObject newUnit = UnitActorFactory.InstantiatePrefab(soldierAsset);

            newUnit.GetComponent<UnitActor>().Pooled = true;
            //newUnit.Faction = faction;
            newUnit.name = faction.name + " Soldier";
            newUnit.transform.parent = null;
            newUnit.GetComponent<Soldier>().NavigationAgent.NavAgent.isStopped = false;
            newUnit.gameObject.SetActive(false);
            soldierList.Add(newUnit.GetComponent<UnitActor>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnReady)
            CooldownSpawn();
        else
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
            unit.gameObject.SetActive(false);
        }
    }
    public void CooldownSpawn()
    {
        if (spawnTimer <= 0)
        {
            spawnTimer = 0;
            spawnReady = true;
        }
        else
        {
            spawnTimer -= Time.deltaTime;
            spawnReady = false;
        }
    }
    public void SpawnRifleman()
    {
        spawnReady = false;
        spawnTimer = spawnCooldown;
        foreach (var infantry in soldierList)
        {
            if (!infantry.isActiveAndEnabled && infantry.Pooled)
            {
                infantry.GetComponent<Animator>().SetBool("IsDead", false);
                infantry.Pooled = false;
                infantry.Dead = false;
                infantry.GetComponent<HealthComponent>().totalHealthPoints = 100;
                infantry.transform.position = soldierSpawnPoint.position;
                infantry.gameObject.SetActive(true);
                infantry.GetComponent<NavigationAgent>().GoToPosition(BaseRallyPoint.transform.position);
                return;
            }
        }
    }

}
