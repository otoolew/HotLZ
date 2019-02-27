using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMovement : MonoBehaviour
{
    public Rigidbody RigidBody;
    public KeyCodeVariable upKey;
    public KeyCodeVariable downKey;
    public float rotationSpeed;
    public float movementSpeed;
    public LayerMask floorMask;

    public void MouseRotation()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out RaycastHit floorHit, 500f, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(playerToMouse);
            lookRotation.x = 0f;
            lookRotation.z = 0f;
            RigidBody.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime));
        }
    }
    public void HorizontalFlight()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        moveDirection = moveDirection.normalized * movementSpeed * 50f * Time.deltaTime;
        RigidBody.AddForce(moveDirection);

        if (upKey.KeyPressValue())
            RigidBody.AddForce(transform.up * movementSpeed);
        if (downKey.KeyPressValue())
            RigidBody.AddForce(-transform.up * movementSpeed);
    }
    public void VerticalFlight()
    {
        if (upKey.KeyPressValue())
            RigidBody.AddForce(transform.up * movementSpeed);
        if (downKey.KeyPressValue())
            RigidBody.AddForce(-transform.up * movementSpeed);
    }
    private void AutoLevel()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y + RotationInput * 2f, 0), Time.time * 1f);
    }
}
