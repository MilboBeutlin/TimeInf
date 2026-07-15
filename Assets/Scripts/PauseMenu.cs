using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//open/closes the PauseMenu
//handles the music buttons of the PauseMenu
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider slider;


    private void Start()
    {
        slider.value = AudioManager.Instance.GetVolume();
        slider.onValueChanged.AddListener(SetVolume);

        SyncMuteButton();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //pauseMenu on/off based on its current state
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            //time flow off / normal speed based on the current state of pauseMenu
            Time.timeScale = pauseMenu.activeSelf ? 0f : 1f;
        }
    }


    private void OnEnable()
    {
        if (AudioManager.Instance == null)
            return;

        slider.SetValueWithoutNotify(AudioManager.Instance.GetVolume());

        slider.onValueChanged.RemoveAllListeners();
        slider.onValueChanged.AddListener(SetVolume);
    }


    public void SetVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void ToggleAudio()
    {
        AudioManager.Instance.ToggleAudio();
    }


    private void SyncMuteButton()
    {
        bool enabled = AudioManager.Instance.IsMusicEnabled();
    }
}