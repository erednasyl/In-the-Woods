using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToAtk2 : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       AttackSystem.instance.canReceiveInput = true;
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackSystem.instance.inputReceived){
            animator.SetTrigger("Attack2");
            AttackSystem.instance.InputManager();
            AttackSystem.instance.inputReceived = false;
        }
    }
}
