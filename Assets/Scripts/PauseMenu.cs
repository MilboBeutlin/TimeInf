using UnityEngine;

// de/activates the pauseMenu
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf); //pauseMenu on/off based on its current state
            Time.timeScale = pauseMenu.activeSelf ? 0f : 1f; //time flow off / normal speed based on the current state of pauseMenu
        } 
    }
}
