using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuarters : MonoBehaviour
{
    [SerializeField] private FactionComponent factionComponent;
    public FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    private void OnTriggerEnter(Collider other)
    {
        Soldier soldier = other.GetComponentInParent<Soldier>();
        if (soldier != null)
        {
            if (soldier.FactionComponent.Alignment != FactionComponent.Alignment)
                Debug.Log("Enemy Entered Base!");
        }
    }
}
