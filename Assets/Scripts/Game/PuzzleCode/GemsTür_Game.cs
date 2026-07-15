
using UnityEngine;
using System.Collections;

// Controls the gem door interaction.
// The player can insert 4 gems to open the door. After opening,
// the player can move through the door to another location.
public class GemsTür_Game : MonoBehaviour
{

    [SerializeField] private GM_Game gm;
    [SerializeField] private GameObject emptyGemSlot;
    [SerializeField] private GameObject fullGemSlot;
    [SerializeField] private Transform targetDoorSP;
    private bool kollidiert = false;
    private bool gemsEingesetzt = false;
    private Camera_Game camera;


    private void Start()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }

    // Shows text or teleports the player through the opened door.
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Game player = other.GetComponent<Player_Game>();
            if (gemsEingesetzt == false)
            {
                gm.ChangeText("Press E to  insert Gems and open Door");
                gm.ShowText(true);
                kollidiert = true;
            }
            else if (targetDoorSP != null && player.CurrentMovement.y != 0)
            {
                other.transform.position = targetDoorSP.position;
                camera.UpdateCamera(LocationID.R10);
            }
        }
    }


    void Update()
    {
        if (gemsEingesetzt == false && kollidiert && Input.GetKeyDown(KeyCode.E) && gm.giveCurrentPlayerItems().TryGetValue(Items.Gem, out int anzahl) && anzahl >= 4)
        {
            gm.ShowText(false);
            gm.RemoveItem(Items.Gem, 4);

            //small animation that you inserted something and the gem gets removed. Signalising that it is open
            emptyGemSlot.SetActive(false);
            Invoke(nameof(OpenDoor), 0.5f);

            gemsEingesetzt = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            kollidiert = false;
            gm?.ShowText(false);
        }

    }

    void OpenDoor()
    {
        fullGemSlot.SetActive(false);
    }

}
