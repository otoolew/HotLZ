using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    public KeyCodeVariable debugSpawnKey;

    [SerializeField] private Territory residingTerritory;
    public Territory ResidingTerritory { get => residingTerritory; set => residingTerritory = value; }

    [SerializeField]
    private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField]
    private int riflemenPoolCount;
    public int RiflemenPoolCount { get => riflemenPoolCount; set => riflemenPoolCount = value; }

    [SerializeField]
    private SoldierSchematic soldierAsset;
    public SoldierSchematic SoldierAsset { get => soldierAsset; set => soldierAsset = value; }

    [SerializeField]
    private Transform soldierSpawnPoint;
    public Transform SoliderSpawnPoint { get => soldierSpawnPoint; set => soldierSpawnPoint = value; }

    [SerializeField]
    private Transform soldierReturnPoint;
    public Transform SoldierReturnPoint { get => soldierReturnPoint; set => soldierReturnPoint = value; }

    [SerializeField]
    private TerritoryCheckPoint barracksCheckPoint;
    public TerritoryCheckPoint BarracksCheckPoint { get => barracksCheckPoint; set => barracksCheckPoint = value; }

    [SerializeField]
    private float spawnCooldown;
    public float SpawnCooldown { get => spawnCooldown; set => spawnCooldown = value; }

    [SerializeField]
    private float spawnTimer;
    public float SpawnTimer { get => spawnTimer; set => spawnTimer = value; }

    [SerializeField]
    private bool spawnReady;
    public bool SpawnReady { get => spawnReady; set => spawnReady = value; }

    public List<Soldier> soldierList = new List<Soldier>();

    // Start is called before the first frame update
    void Start()
    {
        ResidingTerritory = GetComponentInParent<Territory>();
        for (int i = 0; i < riflemenPoolCount; i++)
        {
            GameObject newUnit = SoldierFactory.InstantiatePrefab(soldierAsset);

            newUnit.GetComponent<Soldier>().Pooled = true;
            //newUnit.Faction = faction;
            newUnit.name = faction.name + " Soldier";
            newUnit.transform.parent = null;
            newUnit.GetComponent<Soldier>().NavigationAgent.NavAgent.isStopped = false;
            newUnit.gameObject.SetActive(false);
            soldierList.Add(newUnit.GetComponent<Soldier>());
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

            unit.GetComponent<Soldier>().Pooled = true;
            unit.GetComponent<Soldier>().IsDead = false;           
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
        foreach (var soldier in soldierList)
        {
            if (!soldier.isActiveAndEnabled && soldier.Pooled)
            {
                soldier.GetComponent<Animator>().SetBool("IsDead", false);
                soldier.Pooled = false;
                soldier.IsDead = false;
                soldier.CurrentHP = soldier.MaxHP;
                soldier.transform.position = soldierSpawnPoint.position;
                soldier.CurrentTerritory = residingTerritory;
                soldier.gameObject.SetActive(true);
                soldier.NavigationAgent.GoToPosition(barracksCheckPoint.transform.position);
                return;
            }
        }
    }

}
