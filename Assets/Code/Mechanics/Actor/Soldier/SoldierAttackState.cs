using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : StateMachineBehaviour
{
    SoldierUnit soldier;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier = animator.GetComponent<SoldierUnit>();
        soldier.GetComponent<NavigationAgent>().NavAgent.isStopped = true;
        soldier.GetComponent<NavigationAgent>().NavAgent.velocity = Vector3.zero; // Stop Sliding
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier.AimAtTarget();

        if (soldier.SoldierWeapon.WeaponReady)
            soldier.SoldierWeapon.Fire();
        //unit.GetComponent<SoldierUnit>().AimAtTarget();
        //if (unit.GetComponent<Soldier>().Weapon.WeaponReady)
        //    unit.GetComponent<Soldier>().Weapon.Fire();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //unit.GetComponent<NavigationAgent>().NavAgent.isStopped = false;
    }

}
