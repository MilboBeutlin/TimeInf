using UnityEngine;

public class Runen_Game : MonoBehaviour
{
    private int currentRotation = 0;
    [SerializeField] private int correctRotation;
    [SerializeField] private PuzzleManager_Game puzzleManager;

    void OnMouseDown()
    {
        Debug.Log((currentRotation + 45) % 360);
        currentRotation = (currentRotation + 45) % 360;
        transform.rotation = Quaternion.Euler(0, 0, currentRotation);
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
