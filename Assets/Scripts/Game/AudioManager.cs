using UnityEngine;

//handles the audio and saves the settings values 
//separately from the rest, so it won't reset when the player starts a new Game
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource audioSource;
    private float volume = 0.5f;
    private bool musicEnabled = true;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadSettings();
    }


    public void MusikAbspielen(AudioClip audioClip)
    {

        if (audioClip == null)
        {
            return;
        }

        if (audioSource.clip == audioClip && audioSource.isPlaying)
        {
            return;
        }


        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.loop = true;


        audioSource.Play();
        Apply();
    }

    public void SetMusicVolume(float lautstaerke)
    {
        volume = lautstaerke;
        Apply();
        SaveSettings();
    }


    public void ToggleAudio()
    {
        musicEnabled = !musicEnabled;

        Apply();
        SaveSettings();
    }
    public void SetMusicEnabled(bool value)
    {
        musicEnabled = value;

        Apply();
        SaveSettings();
    }

    public float GetVolume()
    {
        return volume;
    }

    public bool IsMusicEnabled()
    {
        return musicEnabled;
    }


    private void Apply()
    {
        audioSource.volume = musicEnabled ? volume : 0f;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("MusicEnabled", musicEnabled ? 1 : 0);
        PlayerPrefs.Save();
    }


    public void LoadSettings()
    {
        volume = PlayerPrefs.GetFloat("Volume", 0.5f);
        musicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;

        Apply();
    }


}




