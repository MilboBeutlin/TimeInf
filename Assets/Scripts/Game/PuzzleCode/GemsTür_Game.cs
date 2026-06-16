
using UnityEngine;

public class GemsTür_Game : MonoBehaviour
{

     [SerializeField] private GM_Game gm;
     [SerializeField] private GameObject wall;
    [SerializeField] private Transform targetDoorSP;
    private bool kollidiert = false;
  private bool gemsEingesetzt = false;
    private Camera_Game camera;

    
    private void Start()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(gemsEingesetzt == false)
        {
            Debug.Log("E um Gems einzusetzen");
            gm.ChangeText("E um Gems einzusetzen.");
            gm.ShowText(true);
            kollidiert = true;
            }else
            {
                other.transform.position = targetDoorSP.position;
                camera.UpdateCamera("K1");
            }
        }
    }


    void Update()
    {
        if(gemsEingesetzt == false && kollidiert && Input.GetKeyDown(KeyCode.E) && gm.giveCurrentPlayerItems().TryGetValue(Items.Gem, out int anzahl) && anzahl == 4)
        {
           gm.RemoveItem(Items.Gem, 4);
            wall.SetActive(false);
            gemsEingesetzt = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        kollidiert = false;
        gm.ShowText(false);
    }
}
