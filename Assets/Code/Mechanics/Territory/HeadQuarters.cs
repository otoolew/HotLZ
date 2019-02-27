using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuarters : ContestableTerritory
{
    [SerializeField]
    private FactionAlignment currentFaction;
    public override FactionAlignment CurrentFaction { get => currentFaction; set => currentFaction = value; }

    [SerializeField] private DefensePosition[] defensePositions;
    public override DefensePosition[] DefensePositions { get => defensePositions; }

    [SerializeField]
    private Barracks barracks;
    public Barracks Barracks { get => barracks; set => barracks = value; }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void UpdateFactionOwnership()
    {
        Debug.Log("TODO: IMPLEMENT UpdateFactionOwnership()");
    }

    public override DefensePosition ClosestDefensePosition(Transform goTranform)
    {
        throw new System.NotImplementedException();
    }

    public override bool FindFoxhole(Soldier soldier)
    {
        throw new System.NotImplementedException();
    }
}
