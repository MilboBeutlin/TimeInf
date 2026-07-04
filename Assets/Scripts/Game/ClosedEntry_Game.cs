using UnityEngine;
using TMPro;

// Handles a closed entry that can be opened with a specific key.
public class ClosedEntry_Game : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GM_Game gameMaster;
    private bool isColliding = false; //checks if the player is near enough to open


    void Update()
    {
        // Open the passage when the player interacts while carrying the required key.
        if(gameMaster.GetText() == "Press E to use Key" && isColliding && Input.GetKeyDown(KeyCode.E) && gameMaster.giveCurrentPlayerItems().ContainsKey(Items.StrangeKey))
        {
            wall.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(wall.activeSelf && other.CompareTag("Player"))
        {
            gameMaster.ChangeText("Press E to use Key");
            gameMaster.ShowText(true);
            isColliding = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            gameMaster?.ShowText(false);
        }
    }
}
