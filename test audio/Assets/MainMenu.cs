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
        SceneManager.LoadSceneAsync(level1);
        
    }

    public void Option(string Option)
    {
        SceneManager.LoadSceneAsync(Option);
        
    }

    public void Credit()
    {
        SceneManager.LoadSceneAsync(3);
        
    }

    
    public void Pouse()
    {
        SceneManager.LoadSceneAsync(4);
        
        
    }

    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }


 
}
