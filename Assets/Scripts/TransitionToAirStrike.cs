using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToAirStrike : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackSystem.instance.inputReceived){
            AttackSystem.instance.isAttacking = true;
            animator.SetTrigger("Attack");
            AttackSystem.instance.InputManager();
            AttackSystem.instance.inputReceived = false;
        }
    }
}
