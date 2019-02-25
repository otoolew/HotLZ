using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetVision : MonoBehaviour
{

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
            //Soldier soldier = other.GetComponentInParent<Soldier>();
            //if ((soldier != null) && (soldier == currentOccupant))
            //{
            //    entryCollider.enabled = false;
            //    OccupantChanged?.Invoke();
            //}
        }
    }
}
