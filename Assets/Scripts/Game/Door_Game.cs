using UnityEngine;

//teleports the player between two doors  and handles events of special rooms
public class Door_Game : MonoBehaviour
{

    [SerializeField] private Transform targetDoorSP;
    [SerializeField] private LocationID leadsTo;
    [SerializeField] private GameObject darkness;
    [SerializeField] private GameObject mirrorRoomContent;
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
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (targetDoorSP == null)
        {
            return;
        }
        Player_Game player = other.GetComponent<Player_Game>();

        //blocks the player from entering the door if they are not moving in the correct direction
        bool correctDirection = verticalDoor ? player.CurrentMovement.y != 0 : player.CurrentMovement.x != 0;

        if (!correctDirection)
        {
            return;
        }

        HandleSpecialRooms();

        player.transform.position = targetDoorSP.position;
        camera.UpdateCamera(leadsTo);
    }

    private void HandleSpecialRooms() //handles special events that occur when the player enters certain rooms
    {
        if (darkness && leadsTo == LocationID.R5 && model.GetCurrentPlayerItems().ContainsKey(Items.Lighter))
        {
            darkness.SetActive(false);
        }

        if (waterCollider && leadsTo == LocationID.R1 && model.GetPlayerHasSwim())
        {
            waterCollider.enabled = false;
        }

        if (mirrorRoomContent != null && leadsTo == LocationID.R11 )
        {
            mirrorRoomContent.SetActive(false);
        }
    }
}
