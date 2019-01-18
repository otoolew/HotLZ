using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePosition : MonoBehaviour
{
    [SerializeField]
    private Soldier currentOccupant;
    public Soldier CurrentOccupant { get => currentOccupant; set => currentOccupant = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UnitActor"))
        {
            Soldier unit = other.GetComponentInParent<Soldier>();
            if (unit == null)
                return;
            Debug.Log(unit.name + " is defending!");
        }
    }
}
