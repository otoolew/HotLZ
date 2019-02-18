using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(NavigationAgent))]
public class Soldier : UnitActor
{
    [SerializeField] private FactionAlignment faction;
    public override FactionAlignment Faction { get => faction; set => faction = value; }

    [SerializeField] private Enums.UnitType unitType;
    public override Enums.UnitType UnitType { get => unitType; set => unitType = value; }

    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [SerializeField] private NavigationAgent navigationAgent;
    public NavigationAgent NavigationAgent { get => navigationAgent; set => navigationAgent = value; }

    [SerializeField] private HealthComponent healthComponent;
    public override HealthComponent HealthComponent { get => healthComponent; set => healthComponent = value; }

    [SerializeField] private TargettingComponent targettingComponent;
    public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private SoldierWeaponComponent weapon;
    public SoldierWeaponComponent Weapon { get => weapon; set => weapon = value; }

    [SerializeField] private DefensePosition defensePosition;
    public DefensePosition DefensePosition { get => defensePosition; set => defensePosition = value; }

    [SerializeField] private bool dead;
    public override bool Dead { get => dead; set => dead = value; }

    [SerializeField] private bool pooled;
    public override bool Pooled { get => pooled; set => pooled = value; }

    public override event Action<Targetable> OnTargetRemoved;

    // Start is called before the first frame update
    void Start()
    {
        healthComponent = GetComponent<HealthComponent>();
        healthComponent.OnDeath.AddListener(UnitActorDeath);

        targettingComponent.Faction = GetComponentInParent<UnitActor>().Faction;
        targettingComponent.OnAcquiredTarget.AddListener(HandleTargetAcquired);
        targettingComponent.OnLostTarget.AddListener(HandleTargetLost);
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead)
            return;
        animator.SetFloat("MoveVelocity", navigationAgent.NavAgent.velocity.magnitude);
    }

    public void AimAtTarget()
    {
        if (targettingComponent.CurrentTarget == null)
            return;
        // Create a vector from the npc to the target.
        Vector3 rotVector = targettingComponent.CurrentTarget.transform.position - transform.position;

        // Ensure the vector is entirely along the floor plane.
        rotVector.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(rotVector);

        // Set the character's rotation to this new rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 1f);

    }
    public void HandleTargetAcquired(Targetable target)
    {
        animator.SetBool("HasTarget", true);
    }
    public void HandleTargetLost()
    {
        animator.SetBool("HasTarget", false);
    }

    public override void UnitActorDeath()
    {
        dead = true;
        GetComponent<Animator>().SetBool("IsDead", true);
        GetComponent<Animator>().Play("Dead");
        OnTargetRemoved?.Invoke(this);
        StartCoroutine("DeathSequence");
    }
    IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(2.5f);
        Pooled = true;
        gameObject.SetActive(false);
    }
}
