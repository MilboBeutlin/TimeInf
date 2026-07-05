using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Slider slider;

    void Awake()
    {

    }

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
            pauseMenu.SetActive(!pauseMenu.activeSelf); //pauseMenu on/off based on its current state
            Time.timeScale = pauseMenu.activeSelf ? 0f : 1f; //time flow off / normal speed based on the current state of pauseMenu
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


        Debug.Log("Music enabled: " + enabled);
    }
}