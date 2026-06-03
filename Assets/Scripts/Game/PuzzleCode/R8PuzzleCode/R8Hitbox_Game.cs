using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class R8Hitbox_Game : MonoBehaviour
{

    
    [SerializeField] private Model db;
    [SerializeField] private Controller c;
    [SerializeField] private GameObject textFeld;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject boulder;
    [SerializeField] private BoxCollider2D doorCollider;
    private bool istOffen = false;
    private bool isColliding = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textFeld.SetActive(false);
        doorCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && db.GetCurrentPlayerItems().ContainsKey(Items.Bombe) && isColliding == true)
        {
            c.RemoveItem(Items.Bombe, 1);
            doorCollider.enabled = true;
            textFeld.SetActive(false);
            boulder.SetActive(false);
            istOffen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if(istOffen == false)
            {
                text.text = "Press E to use Bomb";
                textFeld.SetActive(true);
            }
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textFeld.SetActive(false);
        }
        isColliding = false;
    }
}
