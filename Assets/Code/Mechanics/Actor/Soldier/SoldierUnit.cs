using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierUnit : Targetable
{
    [SerializeField] private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [SerializeField] private NavigationAgent navigationAgent;
    public NavigationAgent NavigationAgent { get => navigationAgent; set => navigationAgent = value; }

    [SerializeField] private DefensePosition currentDefensePosition;
    public DefensePosition CurrentDefensePosition { get => currentDefensePosition; set => currentDefensePosition = value; }

    [SerializeField] private Targetable currentTarget;
    public Targetable CurrentTarget { get => currentTarget; set => currentTarget = value; }

    [SerializeField] private TargettingComponent targettingComponent;
    public TargettingComponent TargettingComponent { get => targettingComponent; set => targettingComponent = value; }

    [SerializeField] private SoldierWeaponComponent soldierWeapon;
    public SoldierWeaponComponent SoldierWeapon { get => soldierWeapon; set => soldierWeapon = value; }

    /// <summary>
    /// Event fired when soldier occupys a defense position
    /// </summary>
    public event Action<DefensePosition> defensePositionReached;

    /// <summary>
    /// Lazy Load, if necesaary and ensure the NavMeshAgent is disabled
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        LazyLoad();
        navigationAgent.NavAgent.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        targettingComponent.acquiredTarget += HandleTargetAcquired;
    }

    /// <summary>
    /// Updates the agent in its different states, 
    /// Reset destination when path is stale
    /// </summary>
    protected virtual void Update()
    {

    }

    /// <summary>	
    /// Setup all the necessary parameters for this agent from configuration data
    /// </summary>
    public virtual void Initialize()
    {
        ResetPositionData();
        LazyLoad();
        CurrentHealth = MaxHealth;

        navigationAgent.NavAgent.enabled = true;
        navigationAgent.NavAgent.isStopped = false;
    }
    public override void Remove()
    {
        base.Remove();

        if (navigationAgent.NavAgent.enabled)
        {
            navigationAgent.NavAgent.isStopped = true;
        }
        navigationAgent.NavAgent.enabled = false;

        if (currentTarget != null)
        {
            currentTarget.removed -= OnTargetDestroyed;
        }
        //m_AttackAffector.enabled = false;
        currentTarget = null;

        //TODO: Return to Pool
        //Poolable.TryPool(gameObject); 
    }
    protected virtual void HandleTargetAcquired(Targetable targetable)
    {
        currentTarget = targetable;
        animator.SetBool("HasTarget", true);
    }
    /// <summary>
    /// If the target is destroyed while other soldiers attack it, ensure it becomes null
    /// </summary>
    /// <param name="targetable">The target that has been destroyed</param>
    protected virtual void OnTargetDestroyed(DamageableBehaviour targetable)
    {
        if (currentTarget == targetable)
        {
            currentTarget.removed -= OnTargetDestroyed;
            currentTarget = null;
            animator.SetBool("HasTarget", false);
        }
    }
    public void AimAtTarget()
    {
        if (CurrentTarget == null)
            return;
        // Create a vector from the npc to the target.
        Vector3 rotVector = CurrentTarget.targetableTransform.position - transform.position;
        Vector3 aimVector = rotVector;

        // Ensure the vector is entirely along the floor plane.
        rotVector.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(rotVector);

        // Set the character's rotation to this new rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 1f);
    }
    /// <summary>
    /// This is a lazy way of caching several components utilised by the Agent
    /// </summary>
    protected virtual void LazyLoad()
    {
        if (animator == null)
            animator = GetComponent<Animator>();        
        if (navigationAgent == null)      
            navigationAgent = GetComponent<NavigationAgent>();
        if (targettingComponent == null)
            targettingComponent = GetComponentInChildren<TargettingComponent>();
        if (soldierWeapon == null)
            soldierWeapon = GetComponent<SoldierWeaponComponent>();
    }
}
