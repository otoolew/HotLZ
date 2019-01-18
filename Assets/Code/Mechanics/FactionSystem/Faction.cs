using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    [SerializeField]
    private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment
    {
        get { return factionAlignment; }
        set { factionAlignment = value; }
    }

    public string FactionName { get { return FactionAlignment.factionName; } }


    public void ChangeFaction(FactionAlignment newFaction)
    {
        FactionAlignment = newFaction;
    }

    public bool IsAlly(FactionAlignment faction)
    {
        return FactionAlignment.Equals(faction);
    }
    public bool IsEnemy(FactionAlignment faction)
    {
        return FactionAlignment.CanHarm(faction);
    }
}
