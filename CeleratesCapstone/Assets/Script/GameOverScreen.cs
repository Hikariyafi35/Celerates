using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverPanel;
    public bool isGameOver = false;

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerGameOver(){
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame(string level1){
        Time.timeScale = 1f;
        SceneManager.LoadScene(level1);
        GetComponent<Movement>().enabled =true;
    }
    public void MainMenu(string mainMenu){
        SceneManager.LoadScene(mainMenu);
    }
    public void Win(string Win){
        SceneManager.LoadScene(Win);
        Debug.Log("you win go to next level");
    }
}
