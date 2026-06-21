using UnityEngine;

public class Enemy_Game : MonoBehaviour
{
    [SerializeField] public Gegner type;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Creation(Gegner type, Sprite sprite)
    {
        this.type = type;
        spriteRenderer.sprite = sprite;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = other.gameObject.transform.position;
        }
    }

}
