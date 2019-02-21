using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RallyPoint : MonoBehaviour
{
    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private Territory nextTerritory;
    public Territory NextTerritory { get => nextTerritory; set => nextTerritory = value; }

    public int squadCapacity;
    public int squadSize;
    public Stack<UnitActor> squadUnits = new Stack<UnitActor>();
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
            UnitActor unit = other.GetComponentInParent<UnitActor>();
            if (unit == null)
                return;
            if ((unit.UnitType != Enums.UnitType.SOLDIER) || (unit.Faction != faction))
                return;
            RallyUnit(unit);
        }
    }
    public void RallyUnit(UnitActor unit)
    {
        if (squadSize < squadCapacity)
        {
            squadUnits.Push(unit);
            squadSize = squadUnits.Count;
            AssignPosition(unit);
        }
    }
    public void AssignPosition(UnitActor unit)
    {
        int positionIndex = squadUnits.Count;
        unit.GetComponent<NavigationAgent>().GoToPosition(squadPositions[positionIndex-1].transform.position);
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
                squadUnits.Pop().GetComponent<NavigationAgent>().GoToPosition(nextTerritory.blueEntrance.transform.position);
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
