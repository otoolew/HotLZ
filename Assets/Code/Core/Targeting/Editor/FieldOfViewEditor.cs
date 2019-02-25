using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fieldOfView = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fieldOfView.transform.position, Vector3.up, Vector3.forward, 360, fieldOfView.viewRadius);
        Vector3 viewAngleA = fieldOfView.DirectionFromAngle(-fieldOfView.viewAngle / 2, false);
        Vector3 viewAngleB = fieldOfView.DirectionFromAngle(fieldOfView.viewAngle / 2, false);

        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngleA * fieldOfView.viewRadius);
        Handles.DrawLine(fieldOfView.transform.position, fieldOfView.transform.position + viewAngleB * fieldOfView.viewRadius);

        if(fieldOfView.TargettingComponent.CurrentTarget != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(fieldOfView.transform.position, fieldOfView.TargettingComponent.CurrentTarget.transform.position);
        }
    }
}
