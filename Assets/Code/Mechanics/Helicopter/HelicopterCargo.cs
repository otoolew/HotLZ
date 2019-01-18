using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterCargo : MonoBehaviour
{


    [SerializeField]
    private int cargoCapacity;
    public int CargoCapacity { get => cargoCapacity; set => cargoCapacity = value; }

    [SerializeField]
    private int cargoTotal;
    public int CargoTotal { get => cargoTotal; set => cargoTotal = value; }

    [SerializeField]
    private Transform unitDropPoint;
    public Transform UnitDropPoint { get => unitDropPoint; set => unitDropPoint = value; }

    public Stack<UnitActor> loadedUnits = new Stack<UnitActor>();
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
        if (other.isTrigger)
            return;
        UnitActor unit = other.GetComponentInParent<UnitActor>();
        if (unit == null)
            return;
        if (unit.Faction == GetComponentInParent<UnitActor>().Faction)
            LoadUnit(unit);
    }
    public void LoadUnit(UnitActor unit)
    {
        if (cargoTotal < cargoCapacity)
        {
            unit.gameObject.SetActive(false);
            loadedUnits.Push(unit);
            Debug.Log("Loaded " + unit.name);
        }
        cargoTotal = loadedUnits.Count;
    }
    public void UnloadUnit()
    {
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
        cargoTotal = loadedUnits.Count;
    }

}
