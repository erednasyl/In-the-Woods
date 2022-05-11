using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCooldown : MonoBehaviour
{
    //Script para lidar com o intervalo de ataque de qualquer coisa que aja em intervalos
    private float TimeBtwAttack;
    public bool CanAttack(){
        //Se o intervalo de tempo zerar, retorna verdadeiro
        if (TimeBtwAttack <= 0){
                return true;
            }
        //Caso contrário, o tempo é reduzido em Time.deltaTime
        TimeBtwAttack -= Time.deltaTime;
        return false;
    }
    
    /*Função para ser utilizada após o ataque/ação, ela atribui ao tempo de intervalo 
    o tempo de intervalo total*/
    public void ResetCooldown(float value){
        TimeBtwAttack = value;
    }
}
