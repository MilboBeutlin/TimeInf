using UnityEngine;
using System.Collections;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private IEnumerator Start()
    {
        yield return null;
        AudioManager.Instance.MusikAbspielen(audioClip);
    }
}