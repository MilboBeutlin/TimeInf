using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;
   [SerializeField] private AudioSource audioSource;
    private int volume = 1;
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

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MusikAbspielen(AudioClip audioClip)
    {


     audioSource.clip = audioClip;
    audioSource.loop = true;

    if (musicEnabled)
        {
            audioSource.Play();
        }
    }

    public void SetMusicVolume(int lautstaerke)
    {
        volume = lautstaerke;
        audioSource.volume = musicEnabled ? volume : 0;
    }

    public void ToggleAudio(bool eingeschaltet)
    {
        musicEnabled = eingeschaltet;

        if (eingeschaltet)
        {
            audioSource.volume = volume;
        } 
        else
        {
        audioSource.volume = 0;    
        }
    }


}
