using UnityEngine;

// Handles chest interactions.
// A closed chest gives the player an item once, while an already open chest
// only displays a message.
public class Chest_Game : MonoBehaviour
{
    [SerializeField] private Items item;
    [SerializeField] public int amount;
    [SerializeField] private Sprite openChest;
    [SerializeField] private SpriteRenderer sR;
    [SerializeField] private GM_Game gm;
    private bool status;
    private bool isColliding;
    private Controller controller;

    [Header("isOpen: no content, !isOpen: has content")]
    [SerializeField] private bool isOpen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !status)
        {
            isColliding = true;
            gm.ChangeText("Press E to Open Chest");
            gm.ShowText(true);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            gm?.ShowText(false);
        }
    }
    void Update()
    {
        if (isColliding && Input.GetKeyDown(KeyCode.E) && !status && !isOpen)
        {
            sR.sprite = openChest; //Grafik Chest offen

            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(item, amount);

            gm.ItemsGot(item, amount);
            gm.ShowText(false);

            status = true;
        }
        else if (isColliding && Input.GetKeyDown(KeyCode.E) && !status && isOpen)
        {
            gm.ChangeText("It's already open");
        }
    }




}
