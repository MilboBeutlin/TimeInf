using UnityEngine;

public class Mirror_Game : MonoBehaviour
{
    [SerializeField] private Transform targetDoorSP;
    [SerializeField] private string leadsTo;
    [SerializeField] private GameObject mirrorRoom;
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
            if(targetDoorSP != null && player.CurrentMovement.y != 0)
            {
                other.transform.position = targetDoorSP.position; //teleports the Player to other door
                camera.UpdateCamera(leadsTo);
            }
            
            
        }
    }
}
