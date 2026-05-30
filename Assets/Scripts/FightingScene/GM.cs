using UnityEngine;
using System.Collections.Generic;
public class GM : MonoBehaviour
{
    private Model model;
    private ButtonManager bM;

    //Fight logic
    [SerializeField] private bool playerturn;

    //Stats
    [SerializeField] private Attacks[] currentPlayerAttacks;
    [SerializeField] private int[] currentplayerStats;
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;
    [SerializeField] private Statuseffekte[] currentPlayerEffects;

    [SerializeField] private int[] currentopponentStats; //health, attack, armor, speed, dk
    [SerializeField] private Attacks[] currentOponnentAttacks;

    private int timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();

        //Fight Logic
        playerturn = true;

        //Stats
        currentPlayerAttacks = new Attacks[6];
        currentOponnentAttacks = new Attacks[6];
        DoLoad();

    }
    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer--;
        }
    }

    public void DoLoad()
    {
        currentPlayerAttacks = model.GetCurrentPlayerAttacks();
        currentplayerStats = model.GetCurrentPlayerStats();
        currentPlayerItems = model.GetCurrentPlayerItems();
        currentPlayerEffects = model.GetCurrentPlayerEffects();

        currentOponnentAttacks = model.GetCurrentOponnentAttacks();
    }


    public void DoAttack(int y)
    {
        playerturn = false;
        bM.TurnChange(false);

        Attacks i = currentPlayerAttacks[y];
        //Hier hin, alles irgendwie außerhalb von Attacken passiert: z.B. Jeden Zug gift schaden.

        //Hier ALLE Attacen für NUR Spieler rein.
        switch (i)
        {
            case Attacks.NULL:
                break;


            default:
                Debug.Log("Gooner");
                break;
        }

        OponentTurn();

        //playerturn = true;
        //bM.TurnChange(true);
        
    }

    public Attacks givePlayerAttack(int attack)
    {
        return currentPlayerAttacks[attack];
    }

    public bool GetPlayerturn()
    {
        return playerturn;
    }

    public Dictionary<Items, int> giveCurrentPlayerItems()
    {
        return currentPlayerItems;
    }

    public void OponentTurn()
    {
        Attacks i;

        //hier entscheiden welche Attacke gew�hlt wird.
        

        i = currentOponnentAttacks[0];
        //Hier ALLE Attacken für NUR jeden Gegner.
        switch(i)
        {
            case Attacks.NULL:
            break;

        }
    }
}
