using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newFactionMGPaint", menuName = "Faction/Tower Turret Paint")]
public class FactionMGTurretMaterial : ScriptableObject
{
    public Material baseMaterial;
    public Material turretMaterial;

    public void ChangeMaterial(TowerTurret towerTurret)
    {
        towerTurret.transform.Find("Model").transform.Find("Base").GetComponent<MeshRenderer>().material = baseMaterial;
        towerTurret.transform.Find("Model").transform.Find("Turret").GetComponent<MeshRenderer>().material = turretMaterial;
    }
}
