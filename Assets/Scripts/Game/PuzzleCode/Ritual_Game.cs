using UnityEngine;

public class Ritual_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private Controller controller;
    private bool schwertEingesetzt = false;
    private bool kollidiert;
    [SerializeField] private GameObject gegner;
    private GameObject player;
    private Vector2 pos;

   void Update()
    {
        if(kollidiert && !schwertEingesetzt && gm.giveCurrentPlayerItems().ContainsKey(Items.Ritualschwert))
        {


            if(Input.GetKeyDown(KeyCode.E))
            {
            controller.RemoveItem(Items.Ritualschwert, 1);
           controller = FindAnyObjectByType<Controller>();
            controller.AddItem(Items.Phoenixfeder, 1);
            Debug.Log("+1 feder");
            schwertEingesetzt = true;
            gm.ShowText(false);

            }


            if(Input.GetKeyDown(KeyCode.F))
            {
                 schwertEingesetzt = true;
                Instantiate(gegner, player.transform.position, Quaternion.identity);

            }

         
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
   
      if(other.CompareTag("Player") && gm.giveCurrentPlayerItems().ContainsKey(Items.Ritualschwert) && !schwertEingesetzt)
        {
            kollidiert = true;
            gm.ChangeText("E um das Ritual zu stoppen. F um es zu beenden.");
            gm.ShowText(true);

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            kollidiert = false;
            gm.ShowText(false);
        }
    }
   
    private void Awake()
{
    controller = FindAnyObjectByType<Controller>();
    player = GameObject.FindGameObjectWithTag("Player");
}
}
