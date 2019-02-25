using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventTerritoryOwnerChange : UnityEvent<FactionAlignment> { }

public class Territory : MonoBehaviour
{
    [SerializeField] private FactionAlignment currentFaction;
    public FactionAlignment CurrentFaction { get => currentFaction; set => currentFaction = value; }
 
    public RallyPoint blueEntrance;
    public RallyPoint blueExit;

    public RallyPoint redEntrance;
    public RallyPoint redExit;

    public DefensePosition[] defensePositions;
    public Transform[] pathPositions;

    public int blueFactionDefense;
    public int redFactionDefense;

    #region Events and Handlers
    public EventTerritoryOwnerChange OnTerritoryOwnerChange;

    public void HandleTerritoryOwnerChange(FactionAlignment newfaction)
    {
        CurrentFaction = newfaction;
    }
    #endregion  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFactionOwnership()
    {
        blueFactionDefense = 0;
        redFactionDefense = 0;
        for (int i = 0; i < defensePositions.Length; i++)
        {  
            if (defensePositions[i].FactionAlignment.factionName == "Blue")
            {
                blueFactionDefense++;
            }
            if (defensePositions[i].FactionAlignment.factionName == "Red")
            {
                redFactionDefense++;
            }           
        }
        if (blueFactionDefense > redFactionDefense)
            OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.BlueFaction);
        if (redFactionDefense > blueFactionDefense)
            OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.RedFaction);
        if (blueFactionDefense == redFactionDefense)
            OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.NeutralFaction);
    }

    public void FindClosestPath(UnitActor unitActor)
    {
        if (pathPositions.Length > 0)
        {
            float closestDistance = float.MaxValue;

            Transform closestPathResult = null;

            for (int i = 0; i < pathPositions.Length; i++)
            {
                float distance = Vector3.Distance(unitActor.transform.position, pathPositions[i].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPathResult = pathPositions[i];
                }
            }
            unitActor.GetComponent<NavigationAgent>().GoToPosition(closestPathResult.position);
        }
      
    }
}
