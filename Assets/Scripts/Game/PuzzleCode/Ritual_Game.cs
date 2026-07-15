using UnityEngine;

//handles the ritual the player either can finish to summon a miniBoss or stio it to get the phoenix feather
public class Ritual_Game : MonoBehaviour
{
    [SerializeField] private GM_Game gm;
    private Controller controller;
    private bool schwertEingesetzt = false;
    private bool kollidiert;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Sprite enemySprite;
    private GameObject player;
    private Vector2 pos;

    void Update()
    {
        if (kollidiert && !schwertEingesetzt && gm.giveCurrentPlayerItems().ContainsKey(Items.RitualSword))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                controller.RemoveItem(Items.RitualSword, 1);
                controller.AddItem(Items.PhoenixFeather, 1);

                gm.ItemsGot(Items.PhoenixFeather, 1);
                gm.ShowText(false);

                schwertEingesetzt = true;
            }


            if (Input.GetKeyDown(KeyCode.F))
            {
                schwertEingesetzt = true;

                //summons the mini boss
                GameObject enemy = Instantiate(enemyPrefab, player.transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy_Game>().Creation(Gegner.MiniBoss, enemySprite);
            }


        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && gm.giveCurrentPlayerItems().ContainsKey(Items.RitualSword) && !schwertEingesetzt)
        {
            kollidiert = true;
            gm.ChangeText("Press E to stop the ritual. Press F to finish it.");
            gm.ShowText(true);

        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            kollidiert = false;
            if (gm)
            {
                gm.ShowText(false);
            }
        }
    }

    private void Awake()
    {
        controller = FindAnyObjectByType<Controller>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
