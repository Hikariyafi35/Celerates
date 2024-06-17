using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   void Update(){
    if(Input.GetKeyDown(KeyCode.Escape)){
        Resume();
    }
}
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
    public void Resume()
    {
        GameManager.instance.ResumeGame();
        
    }
    public void Continue(string win){
        SceneManager.LoadScene(win);
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
    public void MainnMenu(string mainMenu){
        SceneManager.LoadScene(mainMenu);
    }



}
