using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawner : MonoBehaviour
{
    public GameObject gameObjectPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject spawnedObject = Instantiate(gameObjectPrefab);
            spawnedObject.transform.parent = null;
        }
    }

}
