using UnityEngine;

public class Enemy_Game : MonoBehaviour
{
    [SerializeField] public Gegner type;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = other.gameObject.transform.position;
        }
    }

}
