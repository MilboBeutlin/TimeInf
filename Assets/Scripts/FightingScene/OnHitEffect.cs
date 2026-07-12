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

    public IEnumerator PlayHitEffect()
    {
        GameObject slashEffect = Instantiate(slashPrefab, transform.position, Quaternion.identity);

        spriteRenderer.color = flashColor;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.color = originalColor;

        Destroy(slashEffect);
    }
}
