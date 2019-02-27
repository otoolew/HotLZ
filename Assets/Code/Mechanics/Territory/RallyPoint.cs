using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyPoint : MonoBehaviour
{
    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private Territory territoryResiding;
    public Territory TerritoryResiding { get => territoryResiding; set => territoryResiding = value; }

    [SerializeField] private RallyPoint nextRallyPoint;
    public RallyPoint NextRallyPoint { get => nextRallyPoint; set => nextRallyPoint = value; }

    public int squadCapacity;
    public int squadSize;
    public Stack<Soldier> squadUnits = new Stack<Soldier>();
    public Transform[] squadPositions;
    // Start is called before the first frame update
    void Start()
    {
        TerritoryResiding = MapManager.Instance.ClosestTerritory(transform);
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
            if (soldier == null)
                return;
            if ((soldier.UnitType != Enums.UnitType.SOLDIER) || (soldier.FactionAlignment != faction))
                return;
            RallyUnit(soldier);
            soldier.CurrentTerritory = TerritoryResiding;
        }
    }
    public void RallyUnit(Soldier soldier)
    {
        if (squadSize < squadCapacity)
        {
            squadUnits.Push(soldier);
            squadSize = squadUnits.Count;
            AssignPosition(soldier);
        }
    }
    public void AssignPosition(Soldier soldier)
    {
        int positionIndex = squadUnits.Count;
        soldier.GetComponent<NavigationAgent>().GoToPosition(squadPositions[positionIndex-1].transform.position);
        if (squadSize == squadCapacity)
        {
            GetComponent<Collider>().enabled = false;
            DeploySquad();
        }
    }

    public void DeploySquad()
    {
        for (int i = 1; i <= squadSize; i++)
        {
            try
            {
                squadUnits.Pop().GetComponent<NavigationAgent>().GoToPosition(nextRallyPoint.transform.position);
            }
            catch (System.NullReferenceException)
            {
                //Debug.Log(gameObject.name + " Throw Catch");
            }
            
        }
        squadSize = squadUnits.Count;
        StopCoroutine(TriggerColliderActivation());
        StartCoroutine(TriggerColliderActivation());
    }

    IEnumerator TriggerColliderActivation()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Collider>().enabled = true;
    }
}
