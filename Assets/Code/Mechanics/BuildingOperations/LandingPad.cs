using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPad : MonoBehaviour
{
    [SerializeField]
    private FactionAlignment faction;
    public FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField]
    private HeadQuarters headQuarters;
    public HeadQuarters HeadQuarters { get => headQuarters; set => headQuarters = value; }

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
        Soldier soldier = other.GetComponentInParent<Soldier>();

        if (soldier != null)
        {
            switch (soldier.UnitType)
            {
                case Enums.UnitType.SOLDIER:
                    soldier.GetComponent<NavigationAgent>().GoToPosition(headQuarters.Barracks.SoldierReturnPoint.position);
                    break;
                case Enums.UnitType.HELICOPTER:
                    Debug.Log("TODO: Implement Helicopter landing operations");
                    break;
                case Enums.UnitType.VEHICLE:
                    break;
            }
        }

    }
}
