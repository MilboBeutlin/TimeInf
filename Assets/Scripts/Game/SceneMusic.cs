using UnityEngine;
using System.Collections;

//activates the music with a short delay
public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private IEnumerator Start()
    {
        // Waits one frame to ensure the AudioManager is initialized.
        yield return null;

        AudioManager.Instance.MusikAbspielen(audioClip);
    }
}