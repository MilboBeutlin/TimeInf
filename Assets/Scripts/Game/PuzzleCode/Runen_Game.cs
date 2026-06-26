using UnityEngine;

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
            currentRotation = (currentRotation + 1) % statueSprite.Length;          //change "rotation" of statue. 90 == 1, 180 == 2;...
            spriteRenderer.sprite = statueSprite[currentRotation];             // rotate the statue
        }
        else
        {
            currentRotation = (currentRotation + 45) % 360;                 //increase current rotation by 45 but it stays between 0 and 360 an can't exceed that boundarie
            transform.rotation = Quaternion.Euler(0, 0, currentRotation);   // rotate the rune
        }
        
        puzzleManager.CheckRunen();
    }

    public bool IsCorrect() 
    {
        if(currentRotation == correctRotation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
