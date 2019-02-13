using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private Territory territory;
    public Territory Territory { get => territory; set => territory = value; }

    [SerializeField] private RallyPoint rallyPoint;
    public RallyPoint RallyPoint { get => rallyPoint; set => rallyPoint = value; }

    // Start is called before the first frame update
    void Start()
    {
        territory = GetComponentInParent<Territory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            UnitActor unit = other.GetComponentInParent<UnitActor>();
            if (unit == null)
                return;
            if ((unit.UnitType != Enums.UnitType.SOLDIER) || (unit.Faction != faction))
                return;
            unit.GetComponent<NavigationAgent>().GoToPosition(DirectUnitPosition(unit));
        }
    }
    private Vector3 DirectUnitPosition(UnitActor unitActor)
    {
        if (territory.defensePositions.Length > 0)
        {
            for (int i = 0; i < territory.defensePositions.Length; i++)
            {
                if (territory.defensePositions[i].CurrentOccupant == null)
                    return territory.defensePositions[i].transform.position;
            }
        }

        return rallyPoint.transform.position;
    }
}
