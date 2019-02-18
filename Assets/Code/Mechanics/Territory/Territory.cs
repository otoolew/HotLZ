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

    [SerializeField] private FactionAlignment neutralFaction;
    [SerializeField] private FactionAlignment blueFaction;
    [SerializeField] private FactionAlignment redFaction;

    public EntryPoint blueEntrance;
    public EntryPoint redEntrance;
    public DefensePosition[] defensePositions;
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
            if(defensePositions[i].IsOccupied)
            {
                if (defensePositions[i].Faction.factionName == "Blue")
                {
                    blueFactionDefense++;
                }
                if (defensePositions[i].Faction.factionName == "Red")
                {
                    redFactionDefense++;
                }
            }
        }
        if (blueFactionDefense > redFactionDefense)
            OnTerritoryOwnerChange.Invoke(blueFaction);
        if (redFactionDefense > blueFactionDefense)
            OnTerritoryOwnerChange.Invoke(blueFaction);
        if (blueFactionDefense == redFactionDefense)
            OnTerritoryOwnerChange.Invoke(neutralFaction);
    }

    public bool FindDefensePosition(UnitActor unitActor)
    {
        if (defensePositions.Length > 0)
        {
            for (int i = 0; i < defensePositions.Length; i++)
            {
                if (!defensePositions[i].IsOccupied)
                {
                    unitActor.GetComponent<NavigationAgent>().GoToPosition(defensePositions[i].transform.position);
                    return true;
                }           
            }
        }
        if(unitActor.Faction == blueEntrance.Faction)
        {
            unitActor.GetComponent<NavigationAgent>().GoToPosition(blueEntrance.RallyPoint.transform.position);
            return true;
        }
        if (unitActor.Faction == redEntrance.Faction)
        {
            unitActor.GetComponent<NavigationAgent>().GoToPosition(redEntrance.RallyPoint.transform.position);
            return true;
        }
        return false;
    }

}
