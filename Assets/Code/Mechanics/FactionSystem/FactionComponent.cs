using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEvents;

public class FactionComponent : MonoBehaviour
{
    [SerializeField] private FactionAlignment alignment;
    public FactionAlignment Alignment { get => alignment; set => alignment = value; } // Need to make set private but is coupled.

    public FactionAlignmentChange FactionAlignmentChange;

    public void ChangeFactionAlignment(FactionAlignment newFactionAlignment)
    {
        alignment = newFactionAlignment;
        FactionAlignmentChange.Invoke(newFactionAlignment);
    }
}
