using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionToAtk1 : StateMachineBehaviour
{    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackSystem.instance.inputReceived){
            animator.SetTrigger("Attack");
            AttackSystem.instance.InputManager();
            AttackSystem.instance.inputReceived = false;
        }
    }
}
