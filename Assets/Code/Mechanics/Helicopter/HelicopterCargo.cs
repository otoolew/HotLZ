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

    public Stack<Soldier> loadedUnits = new Stack<Soldier>();
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
        Soldier unit = other.GetComponentInParent<Soldier>();
        if (unit == null)
            return;
        if (unit.FactionAlignment == GetComponentInParent<Soldier>().FactionAlignment)
            LoadUnit(unit);
    }
    public void LoadUnit(Soldier unit)
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
            Soldier unit = loadedUnits.Pop();
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
