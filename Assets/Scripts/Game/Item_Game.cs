using UnityEngine;

public class Item_Game : MonoBehaviour
{
    [SerializeField] private Items item;
    private Controller controller;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //controller = FindAnyObjectByType<Controller>();
            //Set item in Datenbank
            Debug.Log("You gained: " + item);
            Destroy(gameObject);
        }
    }
}
