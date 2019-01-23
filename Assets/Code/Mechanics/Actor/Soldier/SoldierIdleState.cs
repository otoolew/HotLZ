using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : StateMachineBehaviour
{
    UnitActor unit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponent<UnitActor>();
        unit.GetComponent<NavigationAgent>().NavAgent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("MoveVelocity", unit.GetComponent<NavigationAgent>().NavAgent.velocity.magnitude);
        //NPCMovement.NavAgent.SetDestination(NPCMovement.Destination);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
