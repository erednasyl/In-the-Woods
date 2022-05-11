using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceScript : MonoBehaviour
{
    public Collider2D nonTriggerCollider;
    private LevelManager theLevelManager;
    public Sprite completedRock;
    private SpriteRenderer sprite;

    void Start(){
        theLevelManager = FindObjectOfType<LevelManager>();
        sprite = GetComponent<SpriteRenderer>();
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
                if (!theLevelManager.canAdvance){
                    theLevelManager.dialogueBox.SetActive(true);
                    theLevelManager.dialogueText.text = "A stone is missing...";
                }
            }
    }
    void OnTriggerStay2D(Collider2D other){
        //Se pressionar e o jogador tiver o item, o caminho é liberado
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)){
            if (other.tag == "Player"){
                if (theLevelManager.canAdvance){
                    nonTriggerCollider.isTrigger = true;
                    sprite.sprite = completedRock;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        //Apaga caixa de mensagem
        if (other.tag == "Player"){
                if (!theLevelManager.canAdvance){
                    theLevelManager.dialogueBox.SetActive(false);
                    theLevelManager.dialogueText.text = "";
                }
            }
    }
}
