using UnityEngine;

// The hidden door is revealed once the player uses the Ritual Sword.
public class R4_puzzel_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private bool isColliding = false;
    [SerializeField] private GameObject door;
    
    void Start()
    {
        door.SetActive(false);
    }

    void Update()
    {
        if(isColliding && Input.GetKeyUp(KeyCode.E) && gm.giveCurrentPlayerItems().ContainsKey(Items.RitualSword))
        {
            door.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {  
            isColliding = true;
            gm.ChangeText("Press E to use RitualSword and Enter");
            gm.ShowText(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            isColliding = false;
            gm.ShowText(false);
        }
    }
}
