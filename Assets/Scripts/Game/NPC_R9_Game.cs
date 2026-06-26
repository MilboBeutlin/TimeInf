using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPC_R9_Game : MonoBehaviour
{

    //Dieser NPC ist zuerst gefangen R5 und dann nachdem befreien des NPC erscheint er im Secret Raum R9(S1). Er hat eine bool, die bestimmt in welchem Zustand er ist
    private bool isFreed = false;
    //zeigt an wie weit man mit dem Text ist.
    private int Progression;

    [SerializeField] private GameObject NPC_Trapped_Text;
    [SerializeField] private GameObject NPC_TP_location;
    [SerializeField] private GameObject Chest;

    private void Start()
    {
        
    }
    private void Update()
    {
        if(!isFreed)
        {
            NPC_Trapped_Text.GetComponent<Text>().text = "Hello. Please free me. Please! Please! Please!";
        } else
        {
            NPC_Trapped_Text.GetComponent<Text>().text = "Hi thanks for the Rescue again. Look at that Chest that I found!";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        NPC_Trapped_Text.SetActive(true);
        if(isFreed)
        {
            Chest.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            NPC_Trapped_Text?.SetActive(false);
    }

    public void Cagebroken()
    {
        isFreed = true;
        transform.position = Vector2.MoveTowards(transform.position, NPC_TP_location.transform.position, 100);
    }
}
