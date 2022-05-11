using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : StateMachineBehaviour
{
    public float atkSpeed;
    AttackCooldown cooldown;
    bool canAtk;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Chama o script de tempo de recarga
        cooldown = animator.GetComponent<AttackCooldown>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Chama a função de cooldown para verificar se o inimigo pode atacar
        canAtk = cooldown.CanAttack();
        //Se ele puder atacar, muda a animação de estado para ataque (SetTrigger("Attack"))
        if (canAtk){
            animator.SetTrigger("Attack");
            //Utiliza o valor da variável local atkSpeed para resetar o método com o tempo certo
            cooldown.ResetCooldown(atkSpeed);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Garante com que o Trigger não seja ativado duas vezes por acidente
        animator.ResetTrigger("Attack");
    }
}
