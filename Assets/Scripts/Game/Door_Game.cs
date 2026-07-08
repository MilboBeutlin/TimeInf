using UnityEngine;

//teleports the player between two doors
public class Door_Game : MonoBehaviour
{

    [SerializeField] private Transform targetDoorSP;
    [SerializeField] private string leadsTo;
    [SerializeField] private GameObject darkness;
    [SerializeField] private Model model;
    [SerializeField] private BoxCollider2D waterCollider;
    [SerializeField] private bool verticalDoor; // decides whether this door can only be entered vertically or horizontally
    private Camera_Game camera;
    
    private void Awake()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Game player = other.GetComponent<Player_Game>();

            // Door only works if the player enters from the correct direction.
            if(targetDoorSP != null && (verticalDoor && player.CurrentMovement.y != 0) || (!verticalDoor && player.CurrentMovement.x != 0))
            {
                if(leadsTo == "R6" && model.GetCurrentPlayerItems().ContainsKey(Items.Lighter)) // Remove the darkness if the player owns the lighter.
                {
                    darkness.SetActive(false);
                }
                if(leadsTo == "R2" && /* model.GetCurrentPlayerAttacks().Contains(Attacks.Swim)*/ 1==1)  // Remove the water obstacle if the player has learned Swim.
                {
                    waterCollider.enabled = false;
                }
                other.transform.position = targetDoorSP.position; //teleports the Player to other door
                camera.UpdateCamera(leadsTo);
            }
            
            
        }
    }
}
