using UnityEngine;
using System.Collections.Generic;

// Handles the fishing at the lake.
// If the player has a fishing rod, they can catch a scroll once.
public class Lake_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private Controller controller;
    private bool isColliding;
    private bool didFish;

    // Update is called once per frame
    void Update()
    {
        // Gives the player a scroll when fishing is triggered
        if (isColliding && !didFish && Input.GetKeyDown(KeyCode.E))
        {
            controller = FindAnyObjectByType<Controller>();
            controller.AddItem(Items.Scroll, 1);
            gm.ItemsGot(Items.Scroll, 1);
            Debug.Log("You gained: " + "Scroll");
            didFish = true;
            gm.ShowText(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && gm.giveCurrentPlayerItems().ContainsKey(Items.FishingRod) && !didFish)
        {
            isColliding = true;
            gm.ChangeText("Press E to fish");
            gm.ShowText(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            if (gm)
            {
                 gm.ShowText(false);
            }
        }
    }
}
