using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    //Script utilizado no animator da bruxa, ele é o responsável pelos movimentos dela

    public float speed;
    public float atkSpeed;
    Transform player;
    Rigidbody2D mybody;
    Enemy enemy;
    AttackCooldown cooldown;
    bool canAtk;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        mybody = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        cooldown = animator.GetComponent<AttackCooldown>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        Vector2 target = new Vector2 (player.position.x, mybody.position.y);
        Vector2 newPosition = Vector2.MoveTowards(mybody.position, target, speed * Time.fixedDeltaTime);
        mybody.MovePosition(newPosition);
        
        canAtk = cooldown.CanAttack();

        if (Vector2.Distance(player.position, mybody.position) <= enemy.atkRange && canAtk){
            animator.SetTrigger("Attack");
            cooldown.ResetCooldown(atkSpeed);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
