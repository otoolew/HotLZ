using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NavigationAgent))]
public class Soldier : Targetable
{
    [SerializeField] private Enums.UnitType unitType;
    public Enums.UnitType UnitType { get => unitType; set => unitType = value; }

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [SerializeField] private NavigationAgent navigationAgent;
    public NavigationAgent NavigationAgent { get => navigationAgent; set => navigationAgent = value; }

    [SerializeField] private AITargetingComponent targetingComponent;
    public AITargetingComponent TargetingComponent { get => targetingComponent; set => targetingComponent = value; }

    [SerializeField] private SoldierWeaponComponent weapon;
    public SoldierWeaponComponent Weapon { get => weapon; set => weapon = value; }

    [SerializeField] private DefensePosition defensePosition;
    public DefensePosition DefensePosition { get => defensePosition; set => defensePosition = value; }

    [SerializeField] private bool pooled;
    public bool Pooled { get => pooled; set => pooled = value; }

    // Start is called before the first frame update
    void Start()
    {
        targetingComponent.acquiredTarget += OnTargetAcquired;
        targetingComponent.lostTarget += OnTargetLost;
        //targettingComponent.FactionAlignment = GetComponentInParent<UnitActor>().FactionAlignment;
        //unitTargetVision.OnAcquiredTarget.AddListener(HandleTargetAcquired);
        //unitTargetVision.OnLostTarget.AddListener(HandleTargetLost);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            return;
        animator.SetFloat("MoveVelocity", navigationAgent.NavAgent.velocity.magnitude);
    }

    public void AimAtTarget()
    {
        if (targetingComponent.CurrentTarget == null)
        {
            //animator.SetBool("HasTarget", false);
            return;
        }

        // Create a vector from the npc to the target.
        Vector3 rotVector = targetingComponent.CurrentTarget.transform.position - transform.position;

        // Ensure the vector is entirely along the floor plane.
        rotVector.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(rotVector);

        // Set the character's rotation to this new rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 1f);

    }
    public void OnTargetAcquired(Targetable target)
    {
        animator.SetBool("HasTarget", true);
    }
    public void OnTargetLost()
    {
        animator.SetBool("HasTarget", false);
    }

    protected override void OnDeath()
    {
        Pooled = true;
        GetComponent<Animator>().SetBool("IsDead", true);
        GetComponent<Animator>().Play("Dead");
        StartCoroutine("DeathSequence");
    }
    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
