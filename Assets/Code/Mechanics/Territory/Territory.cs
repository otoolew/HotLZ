using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Territory : MonoBehaviour
{
    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }
    public EntryPoint blueEntrance;
    public EntryPoint redEntrance;
    public DefensePosition[] defensePositions;

    public int blueFactionDefense;
    public int redFactionDefense;

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
            if(defensePositions[i].CurrentOccupant != null)
            {
                if (defensePositions[i].CurrentOccupant.Faction.factionName == "Blue")
                {
                    blueFactionDefense++;
                }
                if (defensePositions[i].CurrentOccupant.Faction.factionName == "Red")
                {
                    redFactionDefense++;
                }
            }
        }
    }

    public bool FindDefensePosition(UnitActor unitActor)
    {
        if (defensePositions.Length > 0)
        {
            for (int i = 0; i < defensePositions.Length; i++)
            {
                if (defensePositions[i].CurrentOccupant == null)
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
