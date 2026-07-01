
using UnityEngine;
using System.Collections;

public class GemsTür_Game : MonoBehaviour
{

     [SerializeField] private GM_Game gm;
     [SerializeField] private GameObject emptyGemSlot;
     [SerializeField] private GameObject fullGemSlot;
    [SerializeField] private Transform targetDoorSP;
    private bool kollidiert = false;
  private bool gemsEingesetzt = false;
    private Camera_Game camera;
   //private Vector2 pos;

    
    private void Start()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Game player = other.GetComponent<Player_Game>();
            if(gemsEingesetzt == false)
                {
                    gm.ChangeText("Press E to  insert Gems and open Door");
                    gm.ShowText(true);
                    kollidiert = true;
                }
            else if(targetDoorSP != null && player.CurrentMovement.y != 0)
            {
                    //pos = targetDoorSP.position;
                    //pos += new Vector2(0, 5);
                    other.transform.position = targetDoorSP.position;
                    camera.UpdateCamera("R11");
            }
        }
    }


    void Update()
    {
        if(gemsEingesetzt == false && kollidiert && Input.GetKeyDown(KeyCode.E) && gm.giveCurrentPlayerItems().TryGetValue(Items.Gem, out int anzahl) && anzahl >= 4)
        {
            gm.RemoveItem(Items.Gem, 4);
            emptyGemSlot.SetActive(false);
            Invoke(nameof (OpenDoor), 0.5f);
            gemsEingesetzt = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        kollidiert = false;
        gm?.ShowText(false);
    }

   void OpenDoor()
    {
        fullGemSlot.SetActive(false);
    }
    
}
