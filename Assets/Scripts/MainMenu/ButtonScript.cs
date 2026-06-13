using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
     public void LoadNewScene(string sceneName) //start oder return button
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenSetting()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = pauseMenu.activeSelf? 0:1;
    }

}
