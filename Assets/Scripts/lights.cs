using UnityEngine;
using UnityEngine.Rendering.Universal;

//changes the light levels for a visual animation
public class lights : MonoBehaviour
{
    [SerializeField] private Light2D lightSource;
    [SerializeField] private float startIntensity = 1f;
    [SerializeField] private float variation = 0.05f;
    [SerializeField] private float speed = 0.2f;

    void Update() // changes the light intensity with random speed within the specified variation
    {
        float noise = Mathf.PerlinNoise(Time.time * speed, 0f);
        lightSource.intensity = startIntensity + (noise - 0.5f) * 2f * variation;
    }


    // de/activates lights to have better FPS
    private void OnBecameVisible() 
    {
        lightSource.enabled = true;
    }

    private void OnBecameInvisible()
    {
        lightSource.enabled = false;
    }
}
