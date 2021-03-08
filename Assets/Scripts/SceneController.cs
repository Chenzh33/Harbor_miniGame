using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{


    public void Awake()
    {
     

    }

    public void Start()
    {

        GameManager gm = GameManager.Instance;
    }
  
    public void ResumeGame()
    {

        GameManager.Instance.ResumeGame();
    }

    public void NewGame()
    {

        GameManager.Instance.NewGame();

    }

    public void PauseGame()
    {

        GameManager.Instance.PauseGame();

    }

    public void SaveGame()
    {
        GameManager.Instance.SaveGame();

    }

    public void LoadGame()
    {
        GameManager.Instance.LoadGame();

    }

    public void ExitToMainMenu()
    {
        GameManager.Instance.ExitToMainMenu();
    }
    
    public void ExitGame()
    {

        GameManager.Instance.ExitGame();

    }
}
