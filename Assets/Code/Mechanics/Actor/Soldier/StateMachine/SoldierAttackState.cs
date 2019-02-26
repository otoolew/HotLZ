using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : StateMachineBehaviour
{
    Soldier soldier;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier = animator.GetComponent<Soldier>();
        soldier.GetComponent<NavigationAgent>().NavAgent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier.GetComponent<NavigationAgent>().NavAgent.isStopped = true;
        soldier.GetComponent<Soldier>().AimAtTarget();
        if (soldier.GetComponent<Soldier>().Weapon.WeaponReady)
            soldier.GetComponent<Soldier>().Weapon.Fire();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier.GetComponent<NavigationAgent>().NavAgent.isStopped = false;
    }
}
