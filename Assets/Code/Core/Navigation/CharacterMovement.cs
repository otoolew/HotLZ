using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    public CharacterController Controller { get => controller; set => controller = value; }
    public float movementSpeed = 6.0f;
    public float rotationSpeed = 0.15f;
    public float lookSpeed = 20f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 currentDestination;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClickToMove();
        }
        if (controller.isGrounded)
        {
            moveDirection = currentDestination - transform.position;
            moveDirection.y = 0;

            if (moveDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection.normalized), rotationSpeed);
                //transform.rotation = Quaternion.LookRotation(moveDirection.normalized);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection.normalized * Time.deltaTime * movementSpeed);
    }
    private void ClickToMove()
    {
        //Vector3 mousePosition = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.z);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit rayHit, layerMask))
        {
            currentDestination = rayHit.point;
        }
    }
}
