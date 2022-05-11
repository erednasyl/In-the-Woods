using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    //Esse código trata do input de ataque do jogador
    public static AttackSystem instance;
    public bool canReceiveInput;
    public bool inputReceived;
    public bool isAttacking;

    //Atribui à variável instance o próprio script
    private void Awake(){
        instance = this;
    }

    //Permite com que o input só seja possível após a efetuação do ataque
    public void InputManager(){
        if (!canReceiveInput){
            canReceiveInput = true;
        }
        else if (canReceiveInput) {
            canReceiveInput = false;
        }
    }

    public void isntAttacking(){
        isAttacking=false;
    }
}
