using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TowerTurret))]
public class TowerTurretEditor : Editor
{
    private void OnSceneGUI()
    {
        TowerTurret towerTurret = (TowerTurret)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(towerTurret.transform.position, Vector3.up, Vector3.forward, 360, towerTurret.ViewRadius);
        Vector3 viewAngleA = towerTurret.DirectionFromAngle(-towerTurret.ViewAngle / 2, false);
        Vector3 viewAngleB = towerTurret.DirectionFromAngle(towerTurret.ViewAngle / 2, false);

        Handles.DrawLine(towerTurret.transform.position, towerTurret.transform.position + viewAngleA * towerTurret.ViewRadius);
        Handles.DrawLine(towerTurret.transform.position, towerTurret.transform.position + viewAngleB * towerTurret.ViewRadius);

        if (towerTurret.CurrentTarget != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(towerTurret.transform.position, towerTurret.CurrentTarget.transform.position);
        }
    }
}
