using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyPoint : MonoBehaviour
{
    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private RallyPoint nextRallyPoint;
    public RallyPoint NextRallyPoint { get => nextRallyPoint; set => nextRallyPoint = value; }

    public int squadCapacity;
    public int squadSize;
    public Stack<Soldier> squadUnits = new Stack<Soldier>();
    public Transform[] squadPositions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier unit = other.GetComponentInParent<Soldier>();
            if (unit == null)
                return;
            if ((unit.UnitType != Enums.UnitType.SOLDIER) || (unit.FactionAlignment != faction))
                return;
            RallyUnit(unit);
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
