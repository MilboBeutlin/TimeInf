using UnityEngine;
using UnityEngine.SceneManagement;

//moves credits up the screen and returns to main menu when finished

public class Credits : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private int timeToReturnToMenu = 90;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed); // Move the credits up the screen

        Invoke(nameof(ReturnToMenu), timeToReturnToMenu); // Call the ReturnToMenu method after the specified duration
    }

    void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
