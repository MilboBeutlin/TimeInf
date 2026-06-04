using UnityEngine;
using TMPro;
public class ClosedEntry_Game : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject wall;
    private GM_Game gm;
    private bool isColliding = false;


    void Start()
    {
        gm = FindAnyObjectByType<GM_Game>();
    }

    void Update()
    {
        if(text.text == "Press E to use Key" && isColliding == true && Input.GetKeyDown(KeyCode.E) && gm.giveCurrentPlayerItems().ContainsKey(Items.KomischerSchlüssel))
        {
            gm.RemoveItem(Items.KomischerSchlüssel, 1);
            wall.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(wall.activeSelf)
        {
            gm.ChangeText("Press E to use Key");
            gm.ShowText(true);
            isColliding = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
        gm.ShowText(false);
    }
}
