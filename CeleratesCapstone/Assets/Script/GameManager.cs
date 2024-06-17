using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("pouse", LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("pouse");
    }
}
