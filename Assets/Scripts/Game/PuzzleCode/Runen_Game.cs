using UnityEngine;

// Handles a single rune/statue of the rotation puzzle.
public class Runen_Game : MonoBehaviour
{
    private int currentRotation = 0;
    [SerializeField] private int correctRotation;
    [SerializeField] private RunenStatuePuzzle_Game puzzleManager;
    [SerializeField] private Sprite[] statueSprite;

    [SerializeField] private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (statueSprite.Length != 0)
        {
            currentRotation = (currentRotation + 1) % statueSprite.Length;     // Cycle through all statue sprites (0 -> 1 -> ... -> 0).
            spriteRenderer.sprite = statueSprite[currentRotation];             // rotate the statue by changing the sprite
        }
        else
        {
            currentRotation = (currentRotation + 45) % 360;                 // Rotate the rune by 45° each click and keep the value between 0 and 359.
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);   // rotate the rune
        }
        
        puzzleManager.CheckRunen(); // Check if the puzzle is solved after every rotation.
    }

    public bool IsCorrect() 
    {
        return currentRotation == correctRotation;
    }
}
