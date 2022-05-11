using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //GameObject do canvas
    public GameObject pauseMenu;
    /*Static faz com que a variável seja global || essa variável isPause é responsável para que o jogador não 
    possa inserir inputs de movimento ou derivados enquanto o jogo estiver pausado*/
    public static bool isPause;

    void Start()
    {
        //Nenhum jogo começa pausado, portanto, desativar o objeto no início do jogo
        pauseMenu.SetActive(false);
    }
    
    void Update()
    {
        //Se apertar esc:
        if (Input.GetKeyDown(KeyCode.Escape)){
            //Se o jogo estivar pausado, despausa o jogo
            if (isPause){
                isPause = false;
                ResumeGame();
            }
            //Situação contrária
            else if (!isPause){
                isPause = true;
                PauseGame();
            }
        }
    }
    //Timescale 0 significa que o jogo parou completamente
    public void PauseGame(){
        pauseMenu.SetActive(true);
        Time.timeScale= 0f;
    }

    public void ResumeGame(){
        pauseMenu.SetActive(false);
        Time.timeScale= 1f;
    }
}
