using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newSoldierUniform", menuName = "Faction/Soldier Uniform")]
public class FactionUniform : ScriptableObject
{
    public Material uniformMaterial;

    public void ChangeUniform(GameObject soldier)
    {
        soldier.transform.Find("Model").transform.Find("Head").GetComponent<SkinnedMeshRenderer>().material = uniformMaterial;
        soldier.transform.Find("Model").transform.Find("Body").GetComponent<SkinnedMeshRenderer>().material = uniformMaterial;
    }
}
