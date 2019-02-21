using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFactionManager", menuName = "Faction/Faction Provider")]
public class FactionProvider : ScriptableObject
{
    public FactionAlignment NeutralFaction;
    public FactionAlignment BlueFaction;
    public FactionAlignment RedFaction;

    public List<FactionAlignment> FactionList;

    // Start is called before the first frame update
    void Start()
    {
        FactionList.Add(NeutralFaction);
        FactionList.Add(BlueFaction);
        FactionList.Add(RedFaction);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Material FetchFactionMaterial(FactionAlignment faction)
    {
        foreach (var factionItem in FactionList)
        {
            if (factionItem == faction)
                return factionItem.uniform.uniformMaterial;
        }
        return null;
    }
}
