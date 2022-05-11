using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Script responsável por movimentos do jogador e algumas colisões

    //Atributos
    public float moveSpeed;
    public float jumpSpeed;
    public Rigidbody2D myRigidbody;
    public Animator animator;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    //quantidade de knockback
    public float knockBackForce;
    //tempo de knockback
    private float knockBackTime;
    //saber o tempo de knockback
    public float knockBackTotal;
    //direcao
    public float xmultiplier;
    public float advanc;


    //checkpoint
    public Vector3 respawnPos;

    //level manager
    public LevelManager theLevelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        respawnPos = transform.position;
        theLevelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {
        //Movimento básico do jogador

        //Condição verifica se o jogador está ativo ou não, também verifica se o jogo está pausado
        if (theLevelManager.playerIsActive && !PauseMenu.isPause){
            //Aqui verifica-
            if (knockBackTime<=0){
                if (Input.GetAxisRaw("Horizontal") > 0f && AttackSystem.instance.inputReceived == false){
                    myRigidbody.velocity = new Vector3(moveSpeed+advanc, myRigidbody.velocity.y, 0f);
                    transform.localScale = new Vector3(0.09201343f,0.09201343f,1f);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f && AttackSystem.instance.inputReceived == false){
                    myRigidbody.velocity = new Vector3(-moveSpeed-advanc, myRigidbody.velocity.y, 0f);
                    transform.localScale = new Vector3(-0.09201343f,0.09201343f,1f);
                }
                else
                {
                    /*A variável "advanc" armazena o valor de avanço do personagem, se ele não estiver atacando
                    o valor é zero, portanto o valor de velocidade X continua zero até que ele ATAQUE.

                    ATAQUE: ao atacar, na aba de animator eu coloco manualmente o valor de advanc e o momento de
                    avançar durante o ataque. 
                    
                    O valor 11f é o valor necessário para fazer com que o valor da
                    local Scale se aproxime de 1, assim tornando o resultado da multiplicação com o advance
                    mais previsível.
                    */
                    myRigidbody.velocity = new Vector3(advanc*(transform.localScale.x*11f), myRigidbody.velocity.y, 0f);
                }

                /*Atribui à variável do animator do jogador sua velocidade, condição 
                para mudar para a animação de ataque*/
                animator.SetFloat("speed", Mathf.Abs(myRigidbody.velocity.x));


                //Pulo do jogador

                //Checa se o jogador está no chão
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
                /*Atribui à variável do animator a condição de estar no chão ou não.
                Verifique suas diversas utilidades*/
                if (isGrounded){
                    animator.SetBool("Grounded",true);
                }
                else{
                    animator.SetBool("Grounded",false);
                }
                //Faz com que o jogador saia do chão
                if (Input.GetButtonDown("Jump") && isGrounded){
                    myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                    animator.SetTrigger("Jump");
                }
            }

            /*Se o jogador estiver em knockback, reduzir o tempo de knockback, ao chegar a zero o jogador
            terá acesso aos controles novamente*/
            if (knockBackTime > 0){
                knockBackTime -= Time.deltaTime;
                if (transform.localScale.x > 0){
                    myRigidbody.velocity=new Vector3(-knockBackForce,myRigidbody.velocity.y,0f);
                } else{
                    myRigidbody.velocity=new Vector3(knockBackForce,myRigidbody.velocity.y,0f);
                }
            }
        }
    }
    
    //Função de knockback do jogador a ser utilizada por fontes de dano
    public void KnockBack(){
        knockBackTime = knockBackTotal;
    }

    //Função de colisão com objetos
    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "SceneChanger"){
            theLevelManager.ChangeScenes();
        }
        if (other.tag == "KillPlane"){
            theLevelManager.Dead();
        }
    }

    //função chamada no fim da animação de hurt
    public void resetInvulnerable (){
        theLevelManager.isInvulnerable = false;
    }
}