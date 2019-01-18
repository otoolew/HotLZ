using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(UnitActor))]
[RequireComponent(typeof(NavigationAgent))]
public class Soldier : MonoBehaviour
{

    [SerializeField]
    private UnitActor unitActor;
    public UnitActor UnitActor { get => unitActor; set => unitActor = value; }

    [SerializeField]
    private Animator animator;
    public Animator Animator { get => animator; set => animator = value; }

    [SerializeField]
    private NavigationAgent navigationAgent;
    public NavigationAgent NavigationAgent { get => navigationAgent; set => navigationAgent = value; }

    [SerializeField]
    private TargetController targetController;
    public TargetController TargetController { get => targetController; set => targetController = value; }

    [SerializeField]
    private SoldierWeapon weapon;
    public SoldierWeapon Weapon { get => weapon; set => weapon = value; }

    // Start is called before the first frame update
    void Start()
    {
        targetController.Faction = GetComponent<UnitActor>().Faction;
        targetController.OnAcquiredTarget.AddListener(HandleTargetAcquired);
        targetController.OnLostTarget.AddListener(HandleTargetLost);
    }

    // Update is called once per frame
    void Update()
    {
        if (unitActor.Dead)
            return;
        animator.SetFloat("MoveVelocity", navigationAgent.NavAgent.velocity.magnitude);
    }

    public void AimAtTarget()
    {
        if (targetController.CurrentTarget == null)
            return;
        // Create a vector from the npc to the target.
        Vector3 rotVector = targetController.CurrentTarget.transform.position - transform.position;

        // Ensure the vector is entirely along the floor plane.
        rotVector.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(rotVector);

        // Set the character's rotation to this new rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 1f);

    }
    public void HandleTargetAcquired(UnitActor target)
    {
        animator.SetBool("HasTarget", true);
    }
    public void HandleTargetLost()
    {
        animator.SetBool("HasTarget", false);
    }
}
