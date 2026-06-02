using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class R8Hitbox_Game : MonoBehaviour
{

    
     private Model db;
    private Controller c;
    [SerializeField] private GameObject R8text;
    [SerializeField] private GameObject R8Tür;
    private bool istOffen = false;
    private bool isColliding = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        db = FindAnyObjectByType<Model>();
        c = FindAnyObjectByType<Controller>();
        R8text.SetActive(false);
        R8Tür.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && db.GetCurrentPlayerItems().ContainsKey(Items.Bombe) && isColliding == true)
        {
            c.RemoveItem(Items.Bombe, 1);
            R8Tür.SetActive(true);
            R8text.SetActive(false);
            istOffen = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            if(istOffen == false)
            {
                R8text.SetActive(true);
            }
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            R8text.SetActive(false);
        }
        isColliding = false;
    }
}
