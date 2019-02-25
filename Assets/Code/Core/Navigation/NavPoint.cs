using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavPoint : MonoBehaviour
{
    public NavPoint nextBluePoint;
    public NavPoint nextRedPoint;

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
            Soldier soldier = other.GetComponentInParent<Soldier>();
            if (soldier != null)
            {
                switch (soldier.FactionAlignment.factionAlignmentType)
                {
                    case Enums.FactionAlignmentType.NEUTRAL:
                        break;
                    case Enums.FactionAlignmentType.BLUE:
                       
                        break;
                    case Enums.FactionAlignmentType.RED:
                        break;
                    default:
                        break;
                }

            }

        }
    }
}
