using UnityEngine;

public class R5_Cage_Game : MonoBehaviour
{
    private GM_Game GM;
    [SerializeField] private Sprite cage_broken;
    [SerializeField] private GameObject NPC;

    private bool isTrigger = false;

    private bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GM = FindAnyObjectByType<GM_Game>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isTrigger && !isOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = cage_broken;
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
            GM.ChangeText("Press E to use hammer oder shaufel");
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
