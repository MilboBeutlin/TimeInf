using System.IO;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//manages the buttons in the main menu and the pause menu

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text Title;
    public static bool newGame;
     public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;                    //time resumes normally
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame() //resets all the data and loads the game 
    {
        Datenbank db = FindObjectOfType<Datenbank>();
        db?.NewGame();
        SceneManager.LoadScene(1);
    }

    public void OpenSetting()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf); //pauseMenu on/off based on its current state
        Time.timeScale = pauseMenu.activeSelf? 0:1; //time flow off / normal speed based on the current state of pauseMenu
    }

    public void Update()
    {
        //Secret Easter Egg lol
        if(Title != null)
        {
            if (Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.V))
            {
                Title.text = "Alles gute zum Vatertag Leon";
            }
        }
        
    }

}
