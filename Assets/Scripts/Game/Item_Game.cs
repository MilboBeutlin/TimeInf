using UnityEngine;

//items will be collected if the player touches them
public class Item_Game : MonoBehaviour
{
    [SerializeField] public Items item;
    [SerializeField] public int amount;
    [SerializeField] private Attacks attack;
    private GM_Game gm;
    private Controller controller;

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(item, amount);
            if (attack != Attacks.NULL && FindAnyObjectByType<Model>().GetCurrentPlayerAttacks().Count < 10) //checks if you can collect this attacks and if you have enough space
            {
                controller.AddAttack(attack);
            }
            gm = FindAnyObjectByType<GM_Game>();
            gm.ItemsGot(item, amount);
            Destroy(gameObject);
        }
    }
}
