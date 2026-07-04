using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class OnHitEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color flashColor = Color.red;
    private Color originalColor;
    [SerializeField] private GameObject slashPrefab;
    private GameObject slashEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public async Task PlayHitEffect()
    {
        slashEffect = Instantiate(slashPrefab, transform.position, Quaternion.identity);

        if (spriteRenderer == null)
        {
            return;
        }
        spriteRenderer.color = flashColor;

        await Task.Delay(200);

        if (spriteRenderer == null)
        {
            return;
        }
        spriteRenderer.color = originalColor;

        Destroy(slashEffect);
    }
}
