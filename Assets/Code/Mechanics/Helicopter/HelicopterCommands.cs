using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterCommands : MonoBehaviour
{
    public KeyCodeVariable offerPickUpKey;
    public KeyCodeVariable debugDropKey;

    [SerializeField]
    private int seatCapacity;
    public int SeatCapacity { get => seatCapacity; set => seatCapacity = value; }

    [SerializeField]
    private int seatTotal;
    public int SeatTotal { get => seatTotal; set => seatTotal = value; }

    [SerializeField]
    private Transform unitDropPoint;
    public Transform UnitDropPoint { get => unitDropPoint; set => unitDropPoint = value; }

    [SerializeField]
    private bool grounded;
    public bool Grounded { get => grounded; set => grounded = value; }

    public Stack<UnitActor> loadedUnits = new Stack<UnitActor>();
    [SerializeField]
    private LayerMask layerMask;
    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debugDropKey.KeyDownValue())
            UnloadUnit();
        if (offerPickUpKey.KeyDownValue())
        {
            OfferPickUp();
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = true;
        }
        UnitActor unit = other.GetComponentInParent<UnitActor>();
        if (unit == null)
            return;
        if (unit.Faction == GetComponentInParent<UnitActor>().Faction)
            LoadUnit(unit);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            grounded = false;
        }
    }
    public void OfferPickUp()
    {
        Collider[] troops = Physics.OverlapSphere(transform.position, 10f, layerMask);

        if (troops.Length > 0)
        {
            for (int i = 0; i < troops.Length; i++)
            {
                Soldier unit = troops[i].GetComponentInParent<Soldier>();
                if (unit != null)
                {
                    unit.NavigationAgent.GoToPosition(transform.position);
                }
            }
        }
    }

    public void LoadUnit(UnitActor unit)
    {
        if (seatTotal < seatCapacity)
        {
            unit.gameObject.SetActive(false);
            loadedUnits.Push(unit);
            Debug.Log("Loaded " + unit.name);
        }
        seatTotal = loadedUnits.Count;
    }
    public void UnloadUnit()
    {
        if (!grounded)
            return;
        if (loadedUnits.Count > 0)
        {
            UnitActor unit = loadedUnits.Pop();
            unit.transform.position = unitDropPoint.position;
            unit.gameObject.SetActive(true);
            Debug.Log("Unloaded " + unit.name);
        }
        else
        {
            Debug.Log("Nothing Loaded");
        }
        seatTotal = loadedUnits.Count;
    }
}
