using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    //Script responsável por atualizar a barra de vida
    private Image healthBar;
    public float health;
    private float maxHealth;
    LevelManager thePlayer;

    void Start()
    {
        healthBar = GetComponent<Image>();
        //O valor da vida do personagem está armazenada no LEVEL MANAGER!
        thePlayer = FindObjectOfType<LevelManager>();
        maxHealth = thePlayer.maxPlayerHealth;
    }

    void Update()
    {
        //Atualiza a variável local de saúde para a vida atual do jogador
        health = thePlayer.currentPlayerHealth;
        //Preenche a barra baseada na porcentagem da razão entre a vida atual e vida máxima
        healthBar.fillAmount = health/maxHealth;
    }
}
