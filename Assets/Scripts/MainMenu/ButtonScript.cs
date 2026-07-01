using System.IO;
using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text Title;
    public static bool newGame;
     public void LoadNewScene(string sceneName) //start oder return button
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
         Debug.Log("NewGame Button geklickt");
        newGame = true;
         Datenbank db = FindObjectOfType<Datenbank>();
         db?.Start();
         SceneManager.LoadScene(1);
         
    }

    public void OpenSetting()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf? 0:1;
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
