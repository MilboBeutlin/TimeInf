using UnityEngine;
using System.Collections;

public class SceneMusic : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void Awake()
{
    AudioManager.Instance.MusikAbspielen(audioClip);
}
private IEnumerator Start()
{
    yield return null;
    AudioManager.Instance.MusikAbspielen(audioClip);
}
}