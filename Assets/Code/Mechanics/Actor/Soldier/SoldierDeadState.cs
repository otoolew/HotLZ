﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierDeadState : StateMachineBehaviour
{
    SoldierUnit soldier;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier = animator.GetComponent<SoldierUnit>();
        //soldier.GetComponent<SoldierUnit>().NavigationAgent.NavAgent.isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //soldier.GetComponent<SoldierUnit>().NavigationAgent.NavAgent.isStopped = true;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //soldier.GetComponent<SoldierUnit>().NavigationAgent.NavAgent.isStopped = false;
    }
}
