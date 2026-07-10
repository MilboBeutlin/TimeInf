using UnityEngine;

//makes the pixel fade out and move in a random direction to create a small particle effect.
public class PixelFade : MonoBehaviour
{
    private SpriteRenderer sr;

    public float lifetime = 0.8f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //Initializes the pixel with a color and applies a random movement to create a small particle effect.
    public void Init(Color color)
    {
        sr.color = color;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(Random.Range(1.5f, 3.5f), Random.Range(-0.6f, 1.2f));   // Random speed to the right + Slight vertical variation

        rb.linearVelocity = direction;

        rb.angularVelocity = Random.Range(-250f, 250f);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Slowly makes it fades out over its lifetime.
        float t = Time.deltaTime / lifetime;

        Color c = sr.color;
        c.a -= t;
        sr.color = c;

        transform.localScale *= 0.992f;
    }
}

