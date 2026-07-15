using UnityEngine;

// The player can use a bomb to destroy the cage and free the NPC.
public class R5_Cage_Game : MonoBehaviour
{
    private GM_Game GM;
    private Model model;
    [SerializeField] private GameObject cage;
    [SerializeField] private GameObject NPC;

    private bool isTrigger = false;
    private bool isOpen = false;


    void Start()
    {
        GM = FindAnyObjectByType<GM_Game>();
        model = FindAnyObjectByType<Model>();
    }


    void Update()
    {
        if(isTrigger && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E) && model.GetCurrentPlayerItems().ContainsKey(Items.Bomb))
            {
                cage.SetActive(false);
                
                FindAnyObjectByType<NPC_R9_Game>().Cagebroken();

                isOpen = true;
                isTrigger = false;
                GM.ShowText(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isOpen)
        {
            GM.ShowText(true);
            GM.ChangeText("Press E to use Bomb");
            isTrigger = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isOpen)
        {
            GM.ShowText(false);
            isTrigger = false;
        }
    }
}
