using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

//handlles a boulder blocking the way. The player can destroy the boulder if they have a bomb.
public class R8Hitbox_Game : MonoBehaviour
{
    [SerializeField] private GameObject textFeld;
    [SerializeField] private GameObject boulder;
    [SerializeField] private BoxCollider2D doorCollider;
    [SerializeField] private GM_Game gm;

    private bool istOffen = false;
    private bool isColliding = false;


    void Start()
    {
        gm = FindAnyObjectByType<GM_Game>();
        doorCollider.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && gm.giveCurrentPlayerItems().ContainsKey(Items.Bomb) && isColliding == true)
        {
            gm.RemoveItem(Items.Bomb, 1);
            doorCollider.enabled = true;
            boulder.SetActive(false);
            istOffen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (istOffen == false)
            {
                gm.ChangeText("Press E to use Bomb");
                gm.ShowText(true);
            }
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gm.ShowText(false);
            isColliding = false;
        }

    }
}
