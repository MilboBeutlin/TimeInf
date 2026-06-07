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

    private int GegnerDamageLastRound; //nur relevant für Item Spiegelfragment!!

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
        currentopponentStats = model.GetCurrentOponnentStats();
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
    public Dictionary<Statuseffekte, int> giveCurrentPlayerEffects()
    {
        return currentPlayerEffects;
    }
    // k == true; PlayerEffect || k == false; OponnentEffect || setzt Effecte von Spieler oder Gegner.
    public void SetEffect(Statuseffekte effect, int duration, bool isPlayer)
    {
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

    //Alle Dinge die sich immer wieder holen und ehh gemacht werden müssen, egal ob ein Item oder eine Attacke benutzt wird.
    public void Turn()
    {
        playerturn = false;
        bM.TurnChange(false);

        //hier werden die Effekte die noch vorhanden sind abgehandelt.

        if(currentOpponentEffects.ContainsKey(Statuseffekte.Vergiftet)) {
            currentopponentStats[0] -= 10; // Gift macht jede Runde 10 damage
            currentOpponentEffects[Statuseffekte.Vergiftet] -= 1;
            if (currentOpponentEffects[Statuseffekte.Vergiftet] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Vergiftet);
            }
        } else if (currentPlayerEffects.ContainsKey(Statuseffekte.Vergiftet))
        {
            currentplayerStats[0] -= 10;
            currentPlayerEffects[Statuseffekte.Vergiftet] -= 1;
            if(currentPlayerEffects[Statuseffekte.Vergiftet] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Vergiftet);
            }
        }

  
    }

    public void DoAttack(int y)
    {
        Turn();
        Attacks i = currentPlayerAttacks[y];
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
        
    }

    

    public void OponentTurn()
    {
        Attacks i;
        //Hier timer, der bissl stallt.

        //hier entscheiden welche Attacke gew�hlt wird.
        int r = Random.Range(0, 10);

        if (currentopponentStats[4] == 5)
        {
            if(r >= 5)
            {
                i = currentOponnentAttacks[0];
            } else
            {
                i = currentOponnentAttacks[1];
            }
        }
        else if (currentopponentStats[4] == 30){
            if(r >= 3)
            {
                i = currentOponnentAttacks[0];
            } else
            {
                i = currentOponnentAttacks[1];
            }

        }
        else
        {
            //Nur damit die switch schleife unten nicht rumjammert. :P
            i = Attacks.NULL;
        }


            //Hier ALLE Attacken für NUR jeden Gegner.
            switch (i)
            {
                case Attacks.NULL:
                    break;

                case Attacks.BasicAttack:
                    currentplayerStats[0] -= 15;
                    break;
                case Attacks.KleinerAttack:
                    currentplayerStats[0] -= 5;
                    break;

            }

        playerturn = true;
        bM.TurnChange(true);
    }

    
    public void DoUseItem(Items item)
    {
        Turn();

        if(currentPlayerItems.ContainsKey(item) == false)
        {
            Debug.Log("Der Spieler hat ein Item benutzt, welches nicht im Inventar ist. >:( grrr");
            return;
        }
        int randInt = Random.Range(0, 10);
        switch (item)
        {
            case Items.NULL:
                break;

            case Items.Bier:
                //wütend
                SetEffect(Statuseffekte.Wütend, 2, true);                //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN


                //30% Chance auf vergifted

                if (randInt <= 3)
                {
                    SetEffect(Statuseffekte.Vergiftet, 2, true);         //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN
                }
                break;

            case Items.Giftmolotov:
                SetEffect(Statuseffekte.Vergiftet, 2, false);            //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN

                //20% Chance auf wütend
                if (randInt >= 2)
                {
                    SetEffect(Statuseffekte.Wütend, 2, false);           //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN
                }
                break;

            case Items.Heiltrank:
                currentplayerStats[0] += 40;
                if (currentplayerStats[0] >= 101)
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
                SetEffect(Statuseffekte.Gesegnet, 999, true);
                break;

            case Items.Phoenixfeder:
                if (currentPlayerEffects.ContainsKey(Statuseffekte.Verflucht))
                {
                    SetEffect(Statuseffekte.Verflucht, 0, true);
                }
                break;

            case Items.Münzen:
                currentopponentStats[0] -= 1;
                break;

            case Items.Spiegelfragment:
                currentopponentStats[0] -= GegnerDamageLastRound;
                break;

            case Items.Ziegelstein:
                currentopponentStats[0] -= 40;
                SetEffect(Statuseffekte.Gelähmt, 1, false);
                break;

        }
            currentPlayerItems[item] -= 1;
        if (currentPlayerItems[item] == 0)
        {
            currentPlayerItems.Remove(item);
        }

        OponentTurn();
    }
}
