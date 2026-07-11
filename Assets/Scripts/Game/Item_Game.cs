using UnityEngine;

//items will be collected if the player touches them
public class Item_Game : MonoBehaviour
{
    [SerializeField] public Items item;
    [SerializeField] public int amount;
    private GM_Game gm;
    private Controller controller;
    private Model model;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller = FindAnyObjectByType<Controller>();
            gm = FindAnyObjectByType<GM_Game>();
            model = FindAnyObjectByType<Model>();
            controller.AddItem(item, amount);
            gm.ItemsGot(item, amount);         
            Destroy(gameObject);
        }
    }
}
