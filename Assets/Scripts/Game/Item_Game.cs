using UnityEngine;

public class Item_Game : MonoBehaviour
{
    [SerializeField] public Items item;
    [SerializeField] public int amount;
    private GM_Game gm;
    private Controller controller;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(item, amount);
            gm = FindAnyObjectByType<GM_Game>();
            gm.ItemsGot(item, amount);
            Destroy(gameObject);
        }
    }
}
