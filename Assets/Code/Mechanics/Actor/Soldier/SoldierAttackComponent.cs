using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackComponent : AttackComponent
{
    [SerializeField] private SoldierUnit soldier;
    public SoldierUnit Soldier { get => soldier; set => soldier = value; }

    [SerializeField] private float attackDamage;
    public override float AttackDamage { get => attackDamage; set => attackDamage = value; }

    [SerializeField] private float cooldownTime;
    public float CooldownTime { get => cooldownTime; set => cooldownTime = value; }

    [SerializeField] private float attackTimer;
    public float AttackTimer { get => attackTimer; set => attackTimer = value; }

    [SerializeField] private bool attackReady;
    public bool AttackReady { get => attackReady; set => attackReady = value; }

    [SerializeField] private Transform firePoint;
    public Transform FirePoint { get => firePoint; set => firePoint = value; }

    public LayerMask obstacleLayer;
    public LayerMask targetableLayer;

    public void AimAtTarget()
    {        
        if (soldier.CurrentTarget == null)
            return;
        // Create a vector from the npc to the target.
        Vector3 rotVector = soldier.CurrentTarget.targetableTransform.position - transform.position;
        Vector3 aimVector = rotVector;

        // Ensure the vector is entirely along the floor plane.
        rotVector.y = 0f;

        // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
        Quaternion newRotation = Quaternion.LookRotation(rotVector);

        // Set the character's rotation to this new rotation.
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 1f);
    }

    public override void Cooldown()
    {
        if (attackTimer <= 0)
        {
            attackTimer = 0;
            attackReady = true;
        }
        else
        {
            attackTimer -= Time.deltaTime;
            attackReady = false;
        }
    }

    public override void FireAttack()
    {
        if (!AttackReady)
            return;
        Debug.Log("Attack Fired");
        attackTimer = cooldownTime;
        attackReady = false;
        Ray ray = new Ray
        {
            origin = firePoint.position,
            direction = firePoint.forward,
        };

        if (Physics.Raycast(ray, out RaycastHit rayHit, targetableLayer))
        {
            if (rayHit.collider.GetComponent<DamageZone>() == null)
                return;
            Vector3 hitPoint = rayHit.point;
            Vector3 targetDir = hitPoint - firePoint.position;
            Debug.DrawRay(ray.origin, targetDir);
            rayHit.collider.GetComponent<DamageZone>().damageableBehaviour.TakeDamage(attackDamage, FactionProvider);
        }
    }
}
