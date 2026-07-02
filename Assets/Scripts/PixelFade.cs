using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PixelFade : MonoBehaviour
{


    private SpriteRenderer sr;

    public float lifetime = 0.8f;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Init(Color color)
    {
        sr.color = color;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector2 dir = new Vector2(
            Random.Range(1.5f, 3.5f),     // nach rechts
            Random.Range(-0.6f, 1.2f));   // leicht hoch/runter

        rb.linearVelocity = dir;

        rb.angularVelocity = Random.Range(-250f, 250f);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        float t = Time.deltaTime / lifetime;

        Color c = sr.color;
        c.a -= t;
        sr.color = c;

        transform.localScale *= 0.992f;
    }
}

