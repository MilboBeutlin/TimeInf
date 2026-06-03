using UnityEngine;
using TMPro;
public class ClosedEntry_Game : MonoBehaviour
{
    [SerializeField] private GameObject textFeld;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject wall;
    [SerializeField] private Model model;
    [SerializeField] private Controller controller;


    void Start()
    {
        
    }

    void Update()
    {
        if(text.text == "Press E to use Key" && textFeld.activeSelf == true && Input.GetKeyDown(KeyCode.E) && model.GetCurrentPlayerItems().ContainsKey(Items.KomischerSchlüssel))
        {
            controller.RemoveItem(Items.KomischerSchlüssel, 1);
            wall.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(wall.activeSelf)
        {
            text.text = "Press E to use Key";
            textFeld.SetActive(true);
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        textFeld.SetActive(false);
    }
}
