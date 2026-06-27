using UnityEngine;
//made by Dominik
//moves projectile to the left
public class projectile : MonoBehaviour
{
    private float speed = 13f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
