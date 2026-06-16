using UnityEngine;

public class Lake_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private Controller controller;
    private bool isColliding;
    private bool didFish;
    // Update is called once per frame
    void Update()
    {
        if (isColliding && !didFish && Input.GetKeyDown(KeyCode.E))
        {
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(Items.Schriftrolle, 1);
            Debug.Log("You gained: " + "Schriftrolle");
            didFish = true;
            gm.ShowText(false);
        }
    }
        private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && gm.giveCurrentPlayerItems().ContainsKey(Items.Angelrute))
        {
            isColliding = true;
            gm.ChangeText("Press E to fish");
            gm.ShowText(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isColliding = false;
            gm.ShowText(false);
        }
    }
}
