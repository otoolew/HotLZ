using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AITargetingComponent))]
public class AITargetingComponentEditor : Editor
{
    private void OnSceneGUI()
    {
        AITargetingComponent targettingComponent = (AITargetingComponent)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(targettingComponent.transform.position, Vector3.up, Vector3.forward, 360, targettingComponent.ViewRadius);
        Vector3 viewAngleA = targettingComponent.DirectionFromAngle(-targettingComponent.ViewAngle / 2, false);
        Vector3 viewAngleB = targettingComponent.DirectionFromAngle(targettingComponent.ViewAngle / 2, false);

        Handles.DrawLine(targettingComponent.transform.position, targettingComponent.transform.position + viewAngleA * targettingComponent.ViewRadius);
        Handles.DrawLine(targettingComponent.transform.position, targettingComponent.transform.position + viewAngleB * targettingComponent.ViewRadius);

        if (targettingComponent.CurrentTarget != null)
        {
            Handles.color = Color.red;
            Handles.DrawLine(targettingComponent.transform.position, targettingComponent.CurrentTarget.transform.position);
        }
    }
}
