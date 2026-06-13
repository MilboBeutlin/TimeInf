using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //public static PauseManager Instance;
    [SerializeField] private GameObject pauseMenu;

    /*private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }*/

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf); //UI menu an/aus
            Time.timeScale = pauseMenu.activeSelf ? 0f : 1f; //Zeit anhalten/fortsetzen
        }
    }
}
