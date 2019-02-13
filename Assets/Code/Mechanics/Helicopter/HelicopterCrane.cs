using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HelicopterCrane : MonoBehaviour
{
    [SerializeField] private Animator craneAnimator;
    public Animator CraneAnimator { get => craneAnimator; set => craneAnimator = value; }

    [SerializeField] private Grabber grabber;
    public Grabber Grabber { get => grabber; set => grabber = value; }

    [SerializeField] private bool isLowered;
    public bool IsLowered { get => isLowered; set => isLowered = value; }

    [SerializeField] private TowerCrate attachedCrate;
    public TowerCrate AttachedCrate { get => attachedCrate; set => attachedCrate = value; }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LowerCraneFinished()
    {
        isLowered = true;
    }
    public void RaiseCraneFinished()
    {
        isLowered = false;
        grabber.ReleaseHeldItem();
    }

    public void LowerCrane()
    {
        if (craneAnimator == null)
            return;
        craneAnimator.SetTrigger("LowerCrane");
    }
    public void RaiseCrane()
    {
        if (craneAnimator == null)
            return;
        craneAnimator.SetTrigger("RaiseCrane");
    }
    private void OnTriggerEnter(Collider other)
    {
        TowerCrate towerCrate = other.GetComponent<TowerCrate>();
        if(towerCrate != null)
        {

        }
    }
}
