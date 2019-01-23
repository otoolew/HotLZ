﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAttackState : StateMachineBehaviour
{
    UnitActor unit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponent<UnitActor>();
        unit.GetComponent<NavigationAgent>().NavAgent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit.GetComponent<NavigationAgent>().NavAgent.isStopped = true;
        unit.GetComponent<Soldier>().AimAtTarget();
        if (unit.GetComponent<Soldier>().Weapon.WeaponReady)
            unit.GetComponent<Soldier>().Weapon.FireWeapon();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit.GetComponent<NavigationAgent>().NavAgent.isStopped = false;
    }
}
