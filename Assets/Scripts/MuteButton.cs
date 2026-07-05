using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] private Sprite muteButton;
    [SerializeField] private Sprite unmuteButton;
    [SerializeField] private Image image;

    void Start()
    {
        // Set the initial button sprite based on the audio state
        if (AudioManager.Instance.IsMusicEnabled())
        {
            image.sprite = muteButton;
        }
        else
        {
            image.sprite = unmuteButton;
        }
    }

    public void ToggleAudio()
    {
        AudioManager.Instance.ToggleAudio();

        // Update the button sprite based on the audio state
        if (AudioManager.Instance.IsMusicEnabled())
        {
            image.sprite = muteButton;
        }
        else
        {
            image.sprite = unmuteButton;
        }

    }
}