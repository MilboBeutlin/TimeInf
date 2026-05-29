using UnityEngine;

public class Runen_Game : MonoBehaviour
{
    private int currentRotation = 0;
    [SerializeField] private int correctRotation;
    [SerializeField] private PuzzleManager_Game puzzleManager;

    void OnMouseDown()
    {
        Debug.Log((currentRotation + 45) % 360);
        currentRotation = (currentRotation + 45) % 360;                 //increase current rotation by 45 but it stays between 0 and 360 an can't exceed that boundarie
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);   // rotate the rune
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
