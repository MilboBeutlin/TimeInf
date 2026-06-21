using UnityEngine;

public class R4_puzzel_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private bool isColliding = false;
    [SerializeField] private GameObject door;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gm = FindAnyObjectByType<GM_Game>();
        door.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isColliding && Input.GetKeyUp(KeyCode.E) && gm.giveCurrentPlayerItems().ContainsKey(Items.RitualSword))
        {
            door.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {  
            isColliding = true;
            gm.ChangeText("Press E to use RitualSword and Enter");
            gm.ShowText(true);
            Debug.Log("you are stupid");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {        
        if (collision.CompareTag("Player"))
        {
            isColliding = false;
            gm.ShowText(false);
        }
    }
}
