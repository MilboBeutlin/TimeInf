using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    void Start()
    {
        AudioManager.Instance.MusikAbspielen(audioClip);
    }
}