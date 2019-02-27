using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    public Territory[] territories;
    // Start is called before the first frame update
    void Start()
    {
        territories = FindObjectsOfType<Territory>();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Territory ClosestTerritory(Transform goTransform)
    {
        if (territories.Length <= 0)
            return null;

        Territory closestTerritory = territories[0];

        float closestDistance = float.MaxValue;
        for (int i = 0; i < territories.Length; i++)
        {
            float distance = Vector3.Distance(goTransform.position, territories[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTerritory = territories[i];
            }
        }
        return closestTerritory;
    }
}
