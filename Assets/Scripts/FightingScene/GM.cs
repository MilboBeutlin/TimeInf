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
    [SerializeField] private List<Attacks> currentPlayerAttacks;
    [SerializeField] private int[] currentplayerStats;
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;
    [SerializeField] private Dictionary<Statuseffekte, int> currentPlayerEffects;
    //gegner
    [SerializeField] private int[] currentopponentStats; //health, attack, armor, speed, dk
    [SerializeField] private Attacks[] currentOponnentAttacks;
    [SerializeField] private Dictionary<Statuseffekte, int> currentOpponentEffects;

    private int GegnerDamageLastRound; //nur relevant für Item Spiegelfragment!!

    private int timer;

    private int dk = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();

        //Fight Logic
        playerturn = true;

        //Stats
        currentPlayerAttacks = new List<Attacks>();

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
        if(attack >= currentPlayerAttacks.Count)
        {
            Debug.Log("Der Spieler hat versucht eine Attacke zu benutzen, die er nicht hat. >:( grrr");
            return Attacks.NULL;
        }
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
           currentopponentStats[0] -= 10 / currentopponentStats[4]/dk; // grundschaden 10 / resistenz
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

         if(currentOpponentEffects.ContainsKey(Statuseffekte.Blutend)) {
            currentopponentStats[0] -= currentopponentStats[0] / 10 / currentopponentStats[4]/dk; // 10% schaden / resistenz
            currentOpponentEffects[Statuseffekte.Blutend] -= 1;
            if (currentOpponentEffects[Statuseffekte.Blutend] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Blutend);
            }
        } else if (currentPlayerEffects.ContainsKey(Statuseffekte.Blutend))
        {
            currentplayerStats[0] = currentplayerStats[0] * (9/10);
            currentPlayerEffects[Statuseffekte.Blutend] -= 1;
            if(currentPlayerEffects[Statuseffekte.Blutend] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Blutend);
            }
        }


        if(currentOpponentEffects.ContainsKey(Statuseffekte.Brennend)) {
            currentopponentStats[0] -= currentopponentStats[0] / 13 / currentopponentStats[4]/dk;
            currentplayerStats[1] = currentplayerStats[1] * (9/10);
            currentOpponentEffects[Statuseffekte.Brennend] -= 1;
            if (currentOpponentEffects[Statuseffekte.Brennend] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Brennend);
            }
        } else if (currentPlayerEffects.ContainsKey(Statuseffekte.Brennend))
        {
            currentplayerStats[0] = currentplayerStats[0] * (92/100);
            currentPlayerEffects[Statuseffekte.Brennend] -= 1;
            if(currentPlayerEffects[Statuseffekte.Brennend] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Brennend);
            }
        }

  
        if (currentPlayerEffects.ContainsKey(Statuseffekte.Hoffnungsvoll)) // nur 1 runde
        {
          
            if(currentPlayerEffects[Statuseffekte.Hoffnungsvoll] == 0)
            {
                currentopponentStats[1] = currentplayerStats[1] * 2;
                currentPlayerEffects.Remove(Statuseffekte.Hoffnungsvoll);
            } else {
            currentplayerStats[1] = currentplayerStats[1] / 2; 
            currentPlayerEffects[Statuseffekte.Hoffnungsvoll] = 0;
            }
        }


         if (currentPlayerEffects.ContainsKey(Statuseffekte.Geschützt)) // nur 1 runde
        {
          
            if(currentPlayerEffects[Statuseffekte.Geschützt] == 0)
            {
                currentplayerStats[2] -= 100000;
                currentPlayerEffects.Remove(Statuseffekte.Geschützt);
            } else {
            currentplayerStats[2] += 100000;
            currentPlayerEffects[Statuseffekte.Geschützt] = 0;
            }
        }

         if(currentOpponentEffects.ContainsKey(Statuseffekte.Wütend)) {
            currentopponentStats[1] += 20;
            currentOpponentEffects[Statuseffekte.Wütend] -= 1;
            if (currentOpponentEffects[Statuseffekte.Wütend] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Wütend);
            }
        } else if (currentPlayerEffects.ContainsKey(Statuseffekte.Wütend))
        {
            currentplayerStats[1] += 20;
            currentPlayerEffects[Statuseffekte.Wütend] -= 1;
            if(currentPlayerEffects[Statuseffekte.Wütend] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Wütend);
            }
        }


        if(currentOpponentEffects.ContainsKey(Statuseffekte.Gesegnet)) {
            currentopponentStats[1] -= 20;
            currentOpponentEffects[Statuseffekte.Gesegnet] -= 1;
            if (currentOpponentEffects[Statuseffekte.Gesegnet] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Gesegnet);
            }
        } else if (currentPlayerEffects.ContainsKey(Statuseffekte.Gesegnet))
        {
            currentplayerStats[1] += 15;
            currentPlayerEffects[Statuseffekte.Gesegnet] -= 1;
            if(currentPlayerEffects[Statuseffekte.Gesegnet] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Gesegnet);
            currentPlayerEffects[Statuseffekte.Gesegnet] = 0;
            }
        }
  
    }

    public void DoAttack(int y)
    {
        Turn();

        Attacks selectedAttack;

        if(y >= currentPlayerAttacks.Count)
        {
            Debug.Log("Der Spieler hat versucht eine Attacke zu benutzen, die er nicht hat. >:( grrr");
            selectedAttack = Attacks.NULL;
        }
        else
        {
            selectedAttack = currentPlayerAttacks[y];
        }
        
        //Hier ALLE Attacen für NUR Spieler rein.
        switch (selectedAttack)
        {
            case Attacks.NULL:
                break;

             case Attacks.Schutz:
                SetEffect(Statuseffekte.Geschützt, 1, true);
                break;

            case Attacks.Hammer:
                 currentopponentStats[0] -= 50 / currentopponentStats[2]/100; // 50 schaden / rüstung
                break;


            case Attacks.Verstümmelung:
                currentopponentStats[0] -= 80 / currentopponentStats[2]/100;
                int random1 = Random.Range(0, 100);
                if (random1 <= 10)
                {
                SetEffect(Statuseffekte.Blutend, 3, false);
                }
                break;


             case Attacks.Schlag:
                currentopponentStats[0] -= 35 / currentopponentStats[2]/100;
                int random2 = Random.Range(0, 100);
                if (random2 <= 45)
                {
                SetEffect(Statuseffekte.Gelähmt, 1, false);
                }
            break;

            case Attacks.Giftdolch:
                currentopponentStats[0] -= 15 / currentopponentStats[2]/100;
                SetEffect(Statuseffekte.Vergiftet, 3, false);
                int random3 = Random.Range(0, 100);
                if (random3 <= 15)
                {
                 SetEffect(Statuseffekte.Blutend, 3, false);
                 }
                break;

            case Attacks.Feuerball:
                currentopponentStats[0] -= 45 / currentplayerStats[2]/100;
                SetEffect(Statuseffekte.Brennend, 1, false);
            break;

            case Attacks.Dämpfer:
                currentopponentStats[1] = currentopponentStats[1] * (9/10);
            break; 

            case Attacks.Vergeltung:
                int random4 = Random.Range(30, 100);
                currentopponentStats[0] -= random4 / currentopponentStats[0]/100;
                break;

            case Attacks.Graben:
                currentopponentStats[0] -= 10; // ignoriert rüstung
             break;

             case Attacks.LichtderHoffnung:
                SetEffect(Statuseffekte.Hoffnungsvoll, 1, true);
            break;

            case Attacks.Erlösungsschlag:
                if(currentOpponentEffects.ContainsKey(Statuseffekte.Vergiftet))
                {
                    currentopponentStats[0] -= 85;
                    currentOpponentEffects.Remove(Statuseffekte.Vergiftet);
                } else {
                currentopponentStats[0] -= 60;
                }
            break;

            case Attacks.Reinigung:
                        currentPlayerEffects.Remove(Statuseffekte.Vergiftet);
                        //etc
                        //etc
                        //e
            break;

            case Attacks.Leidensstoß:
                if (currentplayerStats[0] * (8/10) > 0)
                {
                currentplayerStats[0] = currentplayerStats[0] * (8/10);
                currentopponentStats[0] -= 90;
                }
                else Debug.Log("bitte nicht");
            break;

            case Attacks.Erleuchtung:
                if (currentopponentStats[4] > 15)
                {
                     currentopponentStats[4] -= 15;
                } else
                {
                    currentopponentStats[4] = 0;
                }

            break;




            default:
                Debug.Log("Error M10");
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
