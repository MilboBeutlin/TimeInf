using UnityEngine;

public class Item_Game : MonoBehaviour
{
    [SerializeField] private Items item;
    [SerializeField] private int amount;
    private Controller controller;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(item, amount);
            Debug.Log("You gained: " + item);
            Destroy(gameObject);
        }
    }
}
