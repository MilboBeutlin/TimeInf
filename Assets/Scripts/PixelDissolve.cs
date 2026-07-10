using UnityEngine;
using System.Collections;

// Dissolves a sprite from right to left by removing one pixel column at a time. Optionally spawns fading pixel particles during the dissolve effect.
public class PixelDissolve : MonoBehaviour
{
    public GameObject dustPrefab;

    [Header("Speed")]
    public float columnsPerSecond = 70f;

    [Header("Dust")]
    [Range(0,1)]
    public float particleChance = 0.35f;

    private SpriteRenderer sr;

    // Runtime copy of the sprite texture so the original asset is not modified.
    private Texture2D runtimeTexture;

    private Sprite runtimeSprite;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        CreateRuntimeTexture();
        StartDissolve();
    }

    // Creates a writable copy of the sprite texture and replaces the original sprite.
    void CreateRuntimeTexture()
    {
        Sprite original = sr.sprite;

        Rect rect = original.textureRect;

        runtimeTexture = new Texture2D((int)rect.width, (int)rect.height);

        runtimeTexture.filterMode = FilterMode.Point;

        runtimeTexture.SetPixels(original.texture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));

        runtimeTexture.Apply();

        runtimeSprite = Sprite.Create(runtimeTexture, new Rect(0,0,runtimeTexture.width,runtimeTexture.height), new Vector2(original.pivot.x / rect.width, original.pivot.y / rect.height), original.pixelsPerUnit);

        sr.sprite = runtimeSprite;
    }

    // Starts the dissolve animation.
    public void StartDissolve()
    {
        StartCoroutine(DissolveRoutine());
    }

    IEnumerator DissolveRoutine()
    {
        int width = runtimeTexture.width;
        int height = runtimeTexture.height;

        float ppu = sr.sprite.pixelsPerUnit;

        // Removes one column per iteration, starting from the right side.
        for (int x = width - 1; x >= 0; x--)
        {
            for (int y = 0; y < height; y++)
            {
                Color c = runtimeTexture.GetPixel(x, y);

                if (c.a < 0.1f)
                    continue;

                if (Random.value < particleChance)
                {
                    // Converts the current pixel position into world space.
                    Vector3 worldPos = transform.position + new Vector3((x - sr.sprite.pivot.x) / ppu,(y - sr.sprite.pivot.y) / ppu,0);

                    GameObject dust = Instantiate(dustPrefab, worldPos, Quaternion.identity);

                    dust.GetComponent<PixelFade>().Init(c);
                }

                runtimeTexture.SetPixel(x, y, new Color(0,0,0,0));
            }

            runtimeTexture.Apply();

            yield return new WaitForSeconds(1f / columnsPerSecond);
        }

        Destroy(gameObject);
    }
}

