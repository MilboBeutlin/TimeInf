using UnityEngine;

public class Credits : MonoBehaviour
{
    [Header("Scroll-Geschwindigkeit")]
    [SerializeField] private float scrollSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed);
    }
}
