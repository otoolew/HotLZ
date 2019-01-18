// ----------------------------------------------------------------------------
//  William O'Toole 
//  Project: Starship
//  SEPT 2018
// ----------------------------------------------------------------------------
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public CameraSettings cameraSettings;

    private void Start()
    {
        //target = GetComponentInParent<Transform>().transform;
        transform.parent = null;
        transform.Rotate(cameraSettings.tiltAngle);
    }
    private void LateUpdate()
    {
        transform.position = target.position + cameraSettings.offset;
    }
}
