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
        UnitActor unitActor = other.GetComponentInParent<UnitActor>();

        if (unitActor != null)
        {
            switch (unitActor.UnitType)
            {
                case Enums.UnitType.Soldier:
                    unitActor.GetComponent<NavigationAgent>().GoToPosition(headQuarters.Barracks.SoldierReturnPoint.position);
                    break;
                case Enums.UnitType.Helicopter:
                    Debug.Log("TODO: Implement Helicopter landing operations");
                    break;
                case Enums.UnitType.Vehicle:
                    break;
            }
        }

    }
}
