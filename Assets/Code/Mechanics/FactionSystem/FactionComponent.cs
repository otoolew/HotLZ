using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionComponent : MonoBehaviour
{
    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }
}
