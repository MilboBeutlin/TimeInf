using UnityEngine;
using TMPro;
public class ClosedEntry_Game : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private GM_Game gameMaster;
    private bool isColliding = false;


    void Update()
    {
        if(gameMaster.GetText() == "Press E to use Key" && isColliding == true && Input.GetKeyDown(KeyCode.E) && gameMaster.giveCurrentPlayerItems().ContainsKey(Items.StrangeKey))
        {
            gameMaster.RemoveItem(Items.StrangeKey, 1);
            wall.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(wall.activeSelf)
        {
            gameMaster.ChangeText("Press E to use Key");
            gameMaster.ShowText(true);
            isColliding = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
        gameMaster.ShowText(false);
    }
}
