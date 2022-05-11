using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{ 
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public LayerMask interactibleLayer;
    public float startTimeBtwAttack;
    public int atkDamage;
    AttackCooldown cooldown;
    bool canAtk;

    void Start(){
        cooldown = gameObject.GetComponent<AttackCooldown>();
    }
    
    void Update()
    {
        canAtk = cooldown.CanAttack();
        if (canAtk){
            //pode atacar
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)){ //keycode.Z é o que identifica a letra
                AttackSystem.instance.inputReceived = true;
                AttackSystem.instance.canReceiveInput = false;
                //Attacking();
                cooldown.ResetCooldown(startTimeBtwAttack);
            }
            else{
                return;
            }
        }
    }

    //Método chamado durante a animação do jogador 
    public void Attacking()
    {
        //animação de ataque
        //animator.SetTrigger("Attack");
        //detectar inimigos no alcance
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        //dar dano

        //ativar efeito especial 21/03/2021
        foreach (Collider2D enemy in hitEnemies){
            //Se o inimigo estiver vivo
            if (!enemy.GetComponent<Enemy>().dead){
                enemy.GetComponent<Enemy>().TakeDamage(atkDamage);
                enemy.GetComponent<Enemy>().EnemyKnockBack(1,1);
            }
            //Se o inimigo estiver morto
            if (enemy.GetComponent<Enemy>().dead){
                if (enemy.GetComponent<Animator>() != null){
                    enemy.GetComponent<Animator>().SetTrigger("dead");
                    if (enemy.gameObject.tag == "SelfKnockback"){
                        Destroy(enemy.gameObject);
                    }
                }
            }
        }
        //detectar objetos no alcance
        Collider2D[] hitScenery = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, interactibleLayer);
        
        foreach (Collider2D objects in hitScenery){
            //instantiate particles
            objects.gameObject.GetComponent<DropHandler>().DropItem();
            
            Destroy(objects.gameObject,0.15f);
        }
    }
    
    void OnDrawGizmosSelected (){
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}