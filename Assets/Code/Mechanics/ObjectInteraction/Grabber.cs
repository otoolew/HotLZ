using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    [SerializeField] private Transform grabPoint;
    public Transform GrabPoint { get => grabPoint; set => grabPoint = value; }

    [SerializeField] private Grabbable heldItem;
    public Grabbable HeldItem { get => heldItem; set => heldItem = value; }

    [SerializeField] private bool isHoldingItem;
    public bool UnityProperty
    {
        get
        {
            if (heldItem == null)
            {
                isHoldingItem = false;
                return false;
            }
            else
            {
                isHoldingItem = true;
                return true;
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        Grabbable grabbable = other.GetComponentInParent<Grabbable>();
        if (grabbable != null)
        {
            Debug.Log("Try Grab");
            AttachGrabble(grabbable);
        }
    }
    public void AttachGrabble(Grabbable grabbable)
    {
        grabbable.GetComponent<Rigidbody>().isKinematic = true;
        grabbable.transform.parent = transform;
        //grabbable.AttachPoint.SetParent(grabPoint);
        HeldItem = grabbable;
    }
    public void ReleaseHeldItem()
    {
        if (heldItem == null)
            return;
        heldItem.transform.parent = null;
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
    }
}
