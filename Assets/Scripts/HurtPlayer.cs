using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    //Esse script é responsável por tirar a vida do jogador, é utilizado em objetos estáticos
    public int damageToGive;
    private Animator animator;
    private LevelManager theLevelManager;
    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "Player"){
            theLevelManager.HurtPlayer(damageToGive);
            //Se o objeto não tiver animação, essa parte do código não roda (se rodasse daria erro)
            if (animator != null){
                animator.SetTrigger("Attack");
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
