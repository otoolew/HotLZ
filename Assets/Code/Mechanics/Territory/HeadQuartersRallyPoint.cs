using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadQuartersRallyPoint : RallyPoint
{
    [SerializeField] private FactionComponent factionComponent;
    public override FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    [SerializeField] private Territory residingTerritory;
    public override Territory ResidingTerritory { get => residingTerritory; set => residingTerritory = value; }

    [SerializeField] private Territory nextTerritory;
    public Territory NextTerritory { get => nextTerritory; set => nextTerritory = value; }

    [SerializeField] private int unitRallyMax;
    public override int UnitRallyMax { get => unitRallyMax; set => unitRallyMax = value; }

    // Start is called before the first frame update
    void Start()
    {
        residingTerritory = GetComponentInParent<Territory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void RallyUnit(Soldier soldier)
    {

    }

    public override void DeploySquad()
    {
        throw new System.NotImplementedException();
    }
}
