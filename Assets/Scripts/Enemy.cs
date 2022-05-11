using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    //Esse script é um script generalizado para todos os inimigos


    //Atributos
    public int maxHealth;
    private int resistance;
    private int currentHealth;
    public int atkDamage;
    public float atkRange;
    public float aggroRange;
    public float inviTime;

    // Estados 
    public bool dead;
    private bool isFlipped;
    public bool isInvincible;

    // Recursos
    private Rigidbody2D mybody;
    [SerializeField]
    private GameObject player;
    public UnityEvent shock;
    private LevelManager theLevelManager;
    [SerializeField]
    public Animator animator;
    public ProjectileAttack shoot;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        mybody = GetComponent<Rigidbody2D>();
        dead = false;
        theLevelManager = FindObjectOfType<LevelManager>();
        shoot = FindObjectOfType<ProjectileAttack>();
    }

    void Update(){
        LookAtPlayer();
        //Utiliza a função canMove para verificar na aba de animator se o jogador está ou não no campo de visão
        animator.SetBool("PlayerOnSight", CanMove());
    }

    /*Verifica a distância entre o jogador e o inimigo, se estiver no campo de visão do inimigo,
    retorna verdadeiro.*/
    public bool CanMove(){
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer < aggroRange){
            return true;
        }
        else {
            return false;
        }
    }

    //Função que faz com que o inimigo leve dnao
    public void TakeDamage(int damage){
        if (!isInvincible){
            currentHealth -= damage-resistance;
            ShockwaveEvent();
            if (animator!= null){
                animator.SetTrigger("hurt");
            }
            isInvincible= true;
            if (currentHealth <= 0){
                Die();
                dead = true;
            }
        }
        /*Para evitar q o jogador bata no inimigo sem parar sem dar chance do inimigo se defender,
        foi feito um sisteminha pra deixar o inimigo invulnerável por uns segundos, esse invoke é responsável
        por permitir q o inimigo leve dano de novo*/
        Invoke("InviReset",inviTime);
    }

    void InviReset(){
        isInvincible = false;
    }

    //O inimigo morre e o evento de tremer tela é executado
    void Die(){
        ShockwaveEvent();
        Debug.Log("Enemy died");
        //Animação de morte
    }

    //Faz com que inimigos recuem ao receber dano
    public void EnemyKnockBack(float x, float y){
        mybody.AddForce(new Vector2 (x,y), ForceMode2D.Impulse);
    }

    //Faz com que o inimigo morra após o fim da animação, esse método é chamado como animation event
    public void SelfDestroy(){
        Destroy(gameObject);
    }
    //Faz com que o inimigo olhe para o jogador
    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
            flipped.z *= -1;
            if (transform.position.x > player.transform.position.x && isFlipped){
                transform.Rotate(0f,180f,0f);
                transform.localScale = flipped;
                isFlipped = false;
            }
            else if (transform.position.x < player.transform.position.x && !isFlipped) {
                transform.Rotate(0f,180f,0f);
                transform.localScale = flipped;
                isFlipped = true;
            }
    }

    //Invoca o evento de choque da cinemachine
    void ShockwaveEvent()
    {
        shock.Invoke();
    }

    //Verifica se o jogador entrou na zona de dano por contato
    void OnTriggerEnter2D (Collider2D other){
        if (!AttackSystem.instance.isAttacking){
            if (other.tag == "Player"){
                if (gameObject.tag == "SelfKnockback"){
                    EnemyKnockBack(3,3);
                    theLevelManager.HurtPlayerNoKnockback(atkDamage);
                    ShockwaveEvent();
                }
                else
                {
                    theLevelManager.HurtPlayer(atkDamage);
                    ShockwaveEvent();
                }
            }
        }
    }

    //Ataca o jogador
    public void AttackPlayer(){
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer < atkRange){
            theLevelManager.HurtPlayerNoKnockback(atkDamage);
            ShockwaveEvent();
        }
    }

    //Atira projéteis
    public void Shoot(){
        shoot.Shoot();
    }

}
