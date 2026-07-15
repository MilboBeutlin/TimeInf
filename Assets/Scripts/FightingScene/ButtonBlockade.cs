using UnityEngine;
using UnityEngine.UI;

// This class is for a button blockade prefab that can be clicked a certain number of times before being destroyed
public class ButtonBlockade : MonoBehaviour
{
    [SerializeField] private Image image;
    private Sprite[] damageSprites;
    private int clicksRemaining;
    private int spriteIndex;


    //allows to create the prefab with a certain number of clicks needed and a set of sprites to display as damage is taken
    public void Setup(int clicksNeeded, Sprite[] sprites)
    {
        clicksRemaining = clicksNeeded;
        damageSprites = sprites;

        image.sprite = damageSprites[damageSprites.Length - clicksNeeded];
    }

    // Handles the click event for the button blockade
    public void Click()
    {
        clicksRemaining--;

        if (clicksRemaining <= 0)
        {
            Destroy(gameObject);
            return;
        }

        spriteIndex = damageSprites.Length - clicksRemaining;
        if (spriteIndex >= 0 && spriteIndex < damageSprites.Length)
        {
            image.sprite = damageSprites[spriteIndex];
        }

    }
}
