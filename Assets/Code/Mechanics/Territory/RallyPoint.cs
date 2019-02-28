using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyPoint : MonoBehaviour
{
    [SerializeField] private Territory territoryResiding;
    public Territory TerritoryResiding { get => territoryResiding; set => territoryResiding = value; }

    //[SerializeField] private RallyPoint nextBlueRallyPoint;
    //public RallyPoint NextBlueRallyPoint { get => nextBlueRallyPoint; set => nextBlueRallyPoint = value; }

    //[SerializeField] private RallyPoint nextRedRallyPoint;
    //public RallyPoint NextRedRallyPoint { get => nextRedRallyPoint; set => nextRedRallyPoint = value; }

    public int blueSquadCapacity;
    public int blueSquadSize;
    public Stack<Soldier> blueSquadUnits = new Stack<Soldier>();

    public int redSquadCapacity;
    public int redSquadSize;
    public Stack<Soldier> redSquadUnits = new Stack<Soldier>();

    public Transform[] blueSquadPositions;
    public Transform[] redSquadPositions;

    // Start is called before the first frame update
    void Start()
    {
        TerritoryResiding = GetComponentInParent<Territory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            Debug.Log(soldier);
            if (soldier == null)
                return;
            if (soldier.UnitType != Enums.UnitType.SOLDIER)
                return;
            soldier.CurrentTerritory = TerritoryResiding;
            RallyUnit(soldier);
        }
    }
    public void RallyUnit(Soldier soldier)
    {
        switch (soldier.FactionComponent.Alignment.factionAlignmentType)
        {
            case Enums.FactionAlignmentType.NEUTRAL:
                break;
            case Enums.FactionAlignmentType.BLUE:
                if (blueSquadSize < blueSquadCapacity)
                {
                    blueSquadUnits.Push(soldier);
                    blueSquadSize = blueSquadUnits.Count;
                    int positionIndex = blueSquadUnits.Count;
                    soldier.GetComponent<NavigationAgent>().GoToPosition(blueSquadPositions[positionIndex - 1].transform.position);

                    if (blueSquadSize == blueSquadCapacity)
                    {
                        //GetComponent<Collider>().enabled = false;
                        for (int i = 1; i <= blueSquadSize; i++)
                        {
                            try
                            {
                                territoryResiding.DutyRequest(blueSquadUnits.Pop());
                            }
                            catch (System.NullReferenceException)
                            {
                                Debug.Log(gameObject.name + " Rally Throw Catch");
                            }
                        }
                    }
                }
                break;
            case Enums.FactionAlignmentType.RED:
                if (redSquadSize < redSquadCapacity)
                {
                    redSquadUnits.Push(soldier);
                    redSquadSize = redSquadUnits.Count;
                    int positionIndex = redSquadUnits.Count;
                    soldier.GetComponent<NavigationAgent>().GoToPosition(redSquadPositions[positionIndex - 1].transform.position);

                    if (redSquadSize == redSquadCapacity)
                    {
                        GetComponent<Collider>().enabled = false;
                        for (int i = 1; i <= redSquadSize; i++)
                        {
                            try
                            {
                                territoryResiding.DutyRequest(redSquadUnits.Pop());
                            }
                            catch (System.NullReferenceException)
                            {
                                //Debug.Log(gameObject.name + " Throw Catch");
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }


        
    }

    //public void HeadToExitRally(Soldier soldier)
    //{
    //    switch (soldier.FactionComponent.Alignment.factionAlignmentType)
    //    {
    //        case Enums.FactionAlignmentType.NEUTRAL:
    //            break;
    //        case Enums.FactionAlignmentType.BLUE:
    //            soldier.NavigationAgent.GoToPosition(blueExit.transform.position);
    //            break;
    //        case Enums.FactionAlignmentType.RED:
    //            soldier.NavigationAgent.GoToPosition(redExit.transform.position);
    //            break;
    //        default:
    //            break;
    //    }
    //}
    //public void AssignPosition(Soldier soldier)
    //{
    //    int positionIndex = squadUnits.Count;
    //    soldier.GetComponent<NavigationAgent>().GoToPosition(squadPositions[positionIndex-1].transform.position);
    //    if (squadSize == squadCapacity)
    //    {
    //        GetComponent<Collider>().enabled = false;
    //        DeploySquad();
    //    }
    //}

    //public void DeploySquad()
    //{
    //    for (int i = 1; i <= squadSize; i++)
    //    {
    //        try
    //        {
    //            squadUnits.Pop().GetComponent<NavigationAgent>().GoToPosition(nextRedRallyPoint.transform.position);
    //        }
    //        catch (System.NullReferenceException)
    //        {
    //            //Debug.Log(gameObject.name + " Throw Catch");
    //        }
            
    //    }
    //    squadSize = squadUnits.Count;
    //    StopCoroutine(TriggerColliderActivation());
    //    StartCoroutine(TriggerColliderActivation());
    //}

    //IEnumerator TriggerColliderActivation()
    //{
    //    yield return new WaitForSeconds(2f);
    //    GetComponent<Collider>().enabled = true;
    //}
}
