using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    public PlayerController thePlayer;

    public int healPots;
    public int shadowCoin;

    //Health management
    public int maxPlayerHealth;
    public int currentPlayerHealth;

    private bool respawning;
    public bool playerIsActive;
    public bool canAdvance;
    public bool isInvulnerable;

    public Text dialogueText;
    public GameObject dialogueBox;
    public Text coinText;
    public TextMeshProUGUI potText;
    public Image[] itemsDisplay;

    private string sceneName;
    public string sceneTracker;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Scene reloaded");
        thePlayer = FindObjectOfType<PlayerController>();
        currentPlayerHealth = maxPlayerHealth;
        coinText.text = shadowCoin.ToString();
        potText.text = healPots+"x";
        playerIsActive = true;
        canAdvance = false;
        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }

    void Update(){
        if (currentPlayerHealth <= 0 && !respawning){
            Dead();
            respawning = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            ConsumeHealPot();
        }
    }


    public void CollectHealPots(){
        healPots++;
        potText.text = healPots+"x";
    }
    public void CollectShadowCoins(){
        shadowCoin++;
        coinText.text = shadowCoin.ToString();
    }


    public void HurtPlayer(int damage){
        if (!isInvulnerable && playerIsActive){
            //knockback
            thePlayer.KnockBack();
            thePlayer.animator.SetTrigger("isHurt");
            //invencibilidade
            currentPlayerHealth -= damage;
            isInvulnerable= true;
        }
    }

    public void HurtPlayerNoKnockback(int damage){
        if (!isInvulnerable && playerIsActive){
            thePlayer.animator.SetTrigger("isHurt");
            currentPlayerHealth -= damage;
            isInvulnerable= true;
        }
    }

    void ConsumeHealPot(){
        if(healPots > 0){
            healPots--;
            HealPlayer(40);
            potText.text = healPots+"x";
        }
    }
    public void HealPlayer(int heal){
        currentPlayerHealth+=heal;
    }

    //Evento a acontecer qnd o personagem morre, tocando a animação de nocaute e reiniciando o jogo em 3 segundos
    public void Dead(){
        Debug.Log("morreu");
        playerIsActive = false;
        thePlayer.myRigidbody.bodyType = RigidbodyType2D.Static;
        thePlayer.animator.SetTrigger("dead");
        Invoke("RestartLevel", 3f);
    }

    //Reinicia o nível
    private void RestartLevel(){
        currentPlayerHealth = maxPlayerHealth;
        sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    //Por enqt só muda pra a cena 1, depois fazer algo flexível q smp direciona pra a cena seguinte
    //Por enqt o jogador perde o progresso tbm (itens e moedas) seria interessante manter os itens do jogador...
    public void ChangeScenes(){
        SceneManager.LoadScene("Scene01");
    }

}
