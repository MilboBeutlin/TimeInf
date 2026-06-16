using UnityEngine;

public class Door_Game : MonoBehaviour
{

    [SerializeField] private Transform targetDoorSP;
    [SerializeField] private string leadsTo;
    [SerializeField] private GameObject darkness;
    //[SerializeField] private GameObject torches;
    [SerializeField] private Model model;
    [SerializeField] private BoxCollider2D waterCollider;
    [SerializeField] private bool verticalDoor;
    private Camera_Game camera;
    
    private void Start()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(targetDoorSP != null && ((verticalDoor && rb.velocity.y != 0) || (!verticalDoor && rb.velocity.x != 0)))
            {
                if(leadsTo == "R6" && model.GetCurrentPlayerItems().ContainsKey(Items.Feuerzeug))
                {
                    darkness.SetActive(false);
                    //torches.SetActive(true);
                }
                if(leadsTo == "R2" && model.GetCurrentPlayerAttacks().Contains(Attacks.Schwimmen))
                {
                    waterCollider.enabled = false;
                }
                other.transform.position = targetDoorSP.position; //moves Player to other door
                camera.UpdateCamera(leadsTo);
            }
            
            
        }
    }

    private void Awake()
{
    camera = FindAnyObjectByType<Camera_Game>();
}
}
