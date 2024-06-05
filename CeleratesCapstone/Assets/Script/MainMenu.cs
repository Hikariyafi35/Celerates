using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
        
    }

    public void PlayGame(string level1)
    {
        SceneManager.LoadScene(level1);
        
    }

    public void Option(string option)
    {
        SceneManager.LoadScene(option);
        
    }

    public void Credit(string credit)
    {
        SceneManager.LoadScene(credit);
        
    }

    
    // public void Pouse()
    // {
    //     SceneManager.LoadSceneAsync(4);
        
        
    // }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }



}
