using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public GameObjectSchematic objectSchematic;

    [SerializeField] private int poolCount;
    public int PoolCount { get => poolCount; set => poolCount = value; }

    [SerializeField] private Transform spawnPoint;
    public Transform SpawnPoint { get => spawnPoint; set => spawnPoint = value; }

    [SerializeField] private float spawnCooldown;
    public float SpawnCooldown { get => spawnCooldown; set => spawnCooldown = value; }

    [SerializeField] private float spawnTimer;
    public float SpawnTimer { get => spawnTimer; set => spawnTimer = value; }

    [SerializeField] private bool spawnReady;
    public bool SpawnReady { get => spawnReady; set => spawnReady = value; }

    public List<GameObject> gameObjectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject newObject = GameObjectFactory.InstantiatePrefab(objectSchematic);

            newObject.SetActive(false);
            gameObjectList.Add(newObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnReady)
            CooldownSpawn();
        else
            SpawnGameObject();
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

    public void SpawnGameObject()
    {
        spawnReady = false;
        spawnTimer = spawnCooldown;
        foreach (GameObject go in gameObjectList)
        {
            if (!go.activeSelf)
            {
                go.transform.parent = null;
                go.transform.position = spawnPoint.position;
                go.gameObject.SetActive(true);                
                return;
            }
        }
    }
}
