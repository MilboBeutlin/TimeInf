using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float speed = 13f;

    void Update()
    {
        //transform.Translate(Vector3.left * speed * Time.deltaTime);
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
