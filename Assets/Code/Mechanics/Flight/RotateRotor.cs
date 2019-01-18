using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRotor : MonoBehaviour
{
    public Enums.RotorAxisRotation rotateAxis;
    public float currentRotationSpeed;
    // Update is called once per frame
    void Update()
    {
        switch (rotateAxis)
        {
            case Enums.RotorAxisRotation.X:
                transform.Rotate(currentRotationSpeed * Time.deltaTime, 0, 0);
                break;
            case Enums.RotorAxisRotation.Y:
                transform.Rotate(0, currentRotationSpeed * Time.deltaTime, 0);
                break;
            case Enums.RotorAxisRotation.Z:
                transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
                break;
        }
    }
}
