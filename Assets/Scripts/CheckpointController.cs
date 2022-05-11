using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    //Esse script permite que o jogador salve sua posição no mapa após interagir com o Obelisk
    public bool checkpointActive;

    void OnTriggerEnter2D(Collider2D other){
        //Exibir caixa de mensagem para salvar
        if (other.tag == "Player"){
                Debug.Log("Pressione Z para salvar");
            }
    }
    void OnTriggerStay2D(Collider2D other){
        //Se pressionar, o checkpoint é salvo
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)){
            if (other.tag == "Player"){
                checkpointActive = true;
                Debug.Log("Progresso salvo!");
                //(O sistema de salvar o jogo não foi programado ;-;)
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        //Apaga caixa de mensagem
        if (other.tag == "Player"){
                Debug.Log("Mensagem desapareceu");
            }
    }
}
