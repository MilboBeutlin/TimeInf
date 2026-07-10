using UnityEngine;

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
        if(other.CompareTag("Player") && !status)
        {
            isColliding = true;
            gm.ChangeText("Press E to Open Chest");
            gm.ShowText(true);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isColliding = false;
            gm?.ShowText(false);
        }
    }
    void Update()
    {
        if(isColliding && Input.GetKeyDown(KeyCode.E) && !status && !isOpen)
        {
            sR.sprite = openChest; //Grafik Chest offen
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(item, amount);
            gm.ItemsGot(item, amount);
            Debug.Log("You gained: " + item);
            status = true;
            gm.ShowText(false);
        }else if(isColliding && Input.GetKeyDown(KeyCode.E) && !status && isOpen)
        {
            gm.ChangeText("It's already open");
        }
    }
    

    
    
}
