using UnityEngine;
using System.Collections.Generic;
public class GM : MonoBehaviour
{
    private Model model;
    private ButtonManager bM;

    //Fight logic
    [SerializeField] private bool playerturn;

    //Stats
    //player
    [SerializeField] private Attacks[] currentPlayerAttacks;
    [SerializeField] private int[] currentplayerStats;
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;
    [SerializeField] private Dictionary<Statuseffekte, int> currentPlayerEffects;
    //gegner
    [SerializeField] private int[] currentopponentStats; //health, attack, armor, speed, dk
    [SerializeField] private Attacks[] currentOponnentAttacks;
    [SerializeField] private Dictionary<Statuseffekte, int> currentOpponentEffects;

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
        currentPlayerEffects = model.GetPlayerEffects();

        currentOponnentAttacks = model.GetCurrentOponnentAttacks();
        currentOpponentEffects = model.GetOpponentEffects();
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

    // k == true; PlayerEffect || k == false; OponnentEffect
    public void SetEffect(Statuseffekte effect, int duration, bool isPlayer)
    {
        /*if(k == true)
        {
            for (int i = 0; i < currentPlayerEffects.Length; i++)
            {
                if (currentPlayerEffects[i] != Statuseffekte.NULL)
                {
                    currentPlayerEffects[i] = j;
                    return;
                }
            }
            Statuseffekte[] temp = new Statuseffekte[currentPlayerEffects.Length + 1];
            temp[temp.Length] = j;
            currentPlayerEffects = temp;
        } else
        {
            for (int i = 0; i < currentOponentEffects.Length; i++)
            {
                if (currentOponentEffects[i] != Statuseffekte.NULL)
                {
                    currentOponentEffects[i] = j;
                    return;
                }
            }
            Statuseffekte[] temp = new Statuseffekte[currentOponentEffects.Length + 1];
            temp[temp.Length] = j;
            currentPlayerEffects = temp;
        }*/
        if (isPlayer)
        {
            if (currentPlayerEffects.ContainsKey(effect))
            {
                currentPlayerEffects[effect] = duration;
            }
            else
            {
                currentPlayerEffects.Add(effect, duration);
            }
        }
        else
        {
            if (currentOpponentEffects.ContainsKey(effect))
            {
                currentOpponentEffects[effect] = duration;
            }
            else
            {
                currentOpponentEffects.Add(effect, duration);
            }
        }
    }
        public void DoUseItem(Items item)
    {
        int randInt = Random.Range(0, 10);
        switch (item)
        {
            case Items.NULL:
                break;

            case Items.Bier:
                //wütend
                SetEffect(Statuseffekte.Wütend,2, true);                //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN


                //30% Chance auf vergifted
                
                if(randInt <= 3)
                {
                    SetEffect(Statuseffekte.Vergiftet,2, true);         //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN
                }
                break;

            case Items.Giftmolotov:
                SetEffect(Statuseffekte.Vergiftet,2, false);            //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN

                //20% Chance auf wütend
                if(randInt >= 2)
                {
                    SetEffect(Statuseffekte.Wütend,2, false);           //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN
                }
                break;

            case Items.Heiltrank:
                currentplayerStats[0] += 40;
                if(currentplayerStats[0] >= 101)
                {
                    currentplayerStats[0] = 100;
                }
                break;

            case Items.GroßerHeiltrank:
                currentplayerStats[0] += 70;
                if (currentplayerStats[0] >= 101)
                {
                    currentplayerStats[0] = 100;
                }
                break;

            case Items.HeiligesKreuz:
                current
                break;

        }
    }
}
