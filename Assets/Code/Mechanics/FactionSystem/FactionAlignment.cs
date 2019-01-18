using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFactionAlignment", menuName = "Faction/Alignment")]
public class FactionAlignment : ScriptableObject, IFactionProvider
{
    public string factionName;
    /// <summary>
    /// A collection of other alignment objects that we can harm
    /// </summary>
    public List<FactionAlignment> enemies;

    /// <summary>
    /// Gets whether the given alignment is in our known list of opponents
    /// </summary>
    public bool CanHarm(IFactionProvider other)
    {
        if (other == null)
        {
            return true;
        }

        var otherAlignment = other as FactionAlignment;

        return otherAlignment != null && enemies.Contains(otherAlignment);
    }
}
