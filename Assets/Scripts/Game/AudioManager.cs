using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;
   [SerializeField] private AudioSource audioSource;
    private float volume = 0f;
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

  /* void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
*/

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
}


public void ToggleAudio()
{
    musicEnabled = !musicEnabled;

    Apply();

    if (audioSource.isPlaying)
    {
        audioSource.volume = musicEnabled ? volume : 0f;
    }
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


}




