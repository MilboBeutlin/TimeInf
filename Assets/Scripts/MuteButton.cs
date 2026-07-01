using UnityEngine;

public class MuteButton : MonoBehaviour
{
    public void ToggleAudio()
{
    AudioManager.Instance.ToggleAudio();
    
//Debug.Log("gedrücKt");
}
}