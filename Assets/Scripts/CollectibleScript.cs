using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    //Esse script serve somente para a coleta de item, coloque-o em itens coletáveis
    private LevelManager theLevelManager;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
    }
    
    void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "Player"){
            if (gameObject.tag == "PotHeal"){
                theLevelManager.CollectHealPots();
            }
            if (gameObject.tag == "ShadowCoin"){
                theLevelManager.CollectShadowCoins();
            }
            //tag para o item necessário para avançar na porta, pode ser mais complexo dps caso necessário
            //mais de um item 
            if (gameObject.tag == "Advance"){
                theLevelManager.canAdvance = true;
            }
            //um efeito de partícula seria legal aqui
            Destroy(gameObject);
        }
    }
}
