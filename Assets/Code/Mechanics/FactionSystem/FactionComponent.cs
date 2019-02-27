using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEvents;

public class FactionComponent : MonoBehaviour
{
    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; } // Need to make set private but is coupled.

    public FactionAlignmentChange FactionAlignmentChange;

    public void ChangeFactionAlignment(FactionAlignment newFactionAlignment)
    {
        factionAlignment = newFactionAlignment;
        FactionAlignmentChange.Invoke(newFactionAlignment);
    }

}
