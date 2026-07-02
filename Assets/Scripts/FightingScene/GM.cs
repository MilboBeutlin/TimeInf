using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using TMPro;

public class GM : MonoBehaviour
{
    private Model model;
    private Controller controller;
    private ButtonManager bM;

    //Fight logic
    [SerializeField] private bool playerturn;
    private bool coinUsed = false;


    //Stats
    //player
    [SerializeField] private int[] currentplayerStats;
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;
    [SerializeField] private Dictionary<Statuseffekte, int> currentPlayerEffects;

    [SerializeField]  private Attacks[] currentPlayerAttacksArray;
    //gegner
    [SerializeField] private int[] currentopponentStats; //health, attack, armor, speed, dk
    [SerializeField] private Attacks[] currentOponnentAttacks;
    [SerializeField] private Dictionary<Statuseffekte, int> currentOpponentEffects;

    private int GegnerDamageLastRound; //nur relevant für Item Spiegelfragment!!

    private int timer;

    [SerializeField] private GameObject SliderPlayerLife;
    [SerializeField] private GameObject SliderGegnerLife;

    [SerializeField] private Text OponentFeedbackText;
    [SerializeField] private Text AnalyseText;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private Sprite[] enemySprites;
    [SerializeField] private GameObject analysisPanel;
    [SerializeField] private GameObject opponentFeedbackPanel;
    private string[] enemyFeedbackTexts = new string[]{"Eye beam", "Horn attack","Flaming strike", "Heabutt", "Void edge", "Shadow touch", "Hellish Bite", "Leg lunge", "Volcanic Slam", "Magma Burst", "Eclipse", "Phantasma wave", "CrownOfDamnation", "Chaos Lance"};

    //health, attack, armor, speed, dk

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();
        controller = FindAnyObjectByType<Controller>();

        //Fight Logic
        playerturn = true;

        //Stats
        currentPlayerAttacksArray = new Attacks[10];

        currentOponnentAttacks = new Attacks[6];
        DoLoad();
        OponentFeedbackText.text = " ";
        opponentFeedbackPanel.SetActive(false);
        enemyRenderer.sprite = enemySprites[(int)model.GetCurrentOponent() - 1];
    }
    // Update is called once per frame
    void Update()
    {
        SliderPlayerLife.GetComponent<Slider>().value = currentplayerStats[0];
        SliderGegnerLife.GetComponent<Slider>().value = currentopponentStats[0];
        
        if(timer > 0)
        {
            timer--;
        } else
        {
            OponentFeedbackText.text = " ";
            opponentFeedbackPanel.SetActive(false);
            //AnalyseText.text = " ";
        }

        // wenn PlayerHP == 0, dann
        if (currentplayerStats[0] <= 0)
        {
            if (currentPlayerItems.ContainsKey(Items.PhoenixFeather)) {
                currentplayerStats[0] = 30;
                currentPlayerItems[Items.PhoenixFeather] -= 1;
            } else
            {
                currentplayerStats[0] = 100;
                model.Save();
                SceneManager.LoadScene(0);
            }
        }
    }

    //Läd alle relevanten Daten aus der DB in diese Klasse
    public void DoLoad()
    {
        Debug.Log("Attacken in DB: " + model.GetCurrentPlayerAttacks().Count);
Debug.Log("Arraygröße: " + currentPlayerAttacksArray.Length);
        for(int i = 0; i < currentPlayerAttacksArray.Length;i++)
        {
            currentPlayerAttacksArray[i] = Attacks.NULL;
        }
        for(int i = 0; i < model.GetCurrentPlayerAttacks().Count; i++)
        {
            currentPlayerAttacksArray[i] = model.GetCurrentPlayerAttacks()[i];
        }
        
        currentplayerStats = model.GetCurrentPlayerStats();
        currentPlayerItems = model.GetCurrentPlayerItems();
        currentPlayerEffects = model.GetPlayerEffects();
        currentOponnentAttacks = model.GetCurrentOponnentAttacks();
        currentOpponentEffects = model.GetOpponentEffects();
        currentopponentStats = model.GetCurrentOponnentStats();
        SliderGegnerLife.GetComponent<Slider>().maxValue = currentopponentStats[0];
    }

    // Speichert alle Relevanten Daten aus dieser Klasse in die DB

    public void DoSave()
    {

        controller.SetCurrentPlayerStats(currentplayerStats);
        controller.SetPlayerItems(currentPlayerItems);
        controller.SetPlayerEffects(currentPlayerEffects);

        controller.SetCurrentOponnentStats(currentopponentStats);
        
    }
    public Attacks givePlayerAttack(int attack)
    {
        if(attack >= currentPlayerAttacksArray.Length)
        {
            Debug.Log("Der Spieler hat versucht eine Attacke zu benutzen, die er nicht hat. >:( grrr");
            return Attacks.NULL;
        }
        return currentPlayerAttacksArray[attack];
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
           currentopponentStats[0] -= 10; // grundschaden 10 / resistenz
            currentOpponentEffects[Statuseffekte.Vergiftet] -= 1;
            if (currentOpponentEffects[Statuseffekte.Vergiftet] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Vergiftet);
            }
        }
        if (currentPlayerEffects.ContainsKey(Statuseffekte.Vergiftet))
        {
            currentplayerStats[0] -= 10;
            currentPlayerEffects[Statuseffekte.Vergiftet] -= 1;
            if(currentPlayerEffects[Statuseffekte.Vergiftet] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Vergiftet);
            }
        }

         if(currentOpponentEffects.ContainsKey(Statuseffekte.Blutend)) {
            currentopponentStats[0] = (int)(currentopponentStats[0] * 0.9f); // 10% schaden
            currentOpponentEffects[Statuseffekte.Blutend] -= 1;
            if (currentOpponentEffects[Statuseffekte.Blutend] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Blutend);
            }
        }
        if (currentPlayerEffects.ContainsKey(Statuseffekte.Blutend))
        {
            currentplayerStats[0] = (int)(currentplayerStats[0] * 0.9f);
            currentPlayerEffects[Statuseffekte.Blutend] -= 1;
            if(currentPlayerEffects[Statuseffekte.Blutend] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Blutend);
            }
        }


        if(currentOpponentEffects.ContainsKey(Statuseffekte.Brennend)) {
            currentopponentStats[0] = (int)(currentopponentStats[0] * 0.92f);
            //currentplayerStats[1] = currentplayerStats[1] * (9/10);// wird es resettet?
            currentOpponentEffects[Statuseffekte.Brennend] -= 1;
            if (currentOpponentEffects[Statuseffekte.Brennend] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Brennend);
            }
        }
        if (currentPlayerEffects.ContainsKey(Statuseffekte.Brennend))
        {
            currentplayerStats[0] = (int)(currentplayerStats[0] * 0.92f);
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
                currentplayerStats[1] = currentplayerStats[1] * 2;
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
        }
        if (currentPlayerEffects.ContainsKey(Statuseffekte.Wütend))
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
        }
        if (currentPlayerEffects.ContainsKey(Statuseffekte.Gesegnet))
        {
            currentplayerStats[1] += 15;
            currentPlayerEffects[Statuseffekte.Gesegnet] -= 1;
            if(currentPlayerEffects[Statuseffekte.Gesegnet] == 0)
            {
                currentPlayerEffects.Remove(Statuseffekte.Gesegnet);
            currentPlayerEffects[Statuseffekte.Gesegnet] = 0;
            }
        }
        if (currentOpponentEffects.ContainsKey(Statuseffekte.Gelähmt))
        {
            coinUsed = true;
            if (currentOpponentEffects[Statuseffekte.Gelähmt] == 0)
            {
                currentOpponentEffects.Remove(Statuseffekte.Gelähmt);
            }
        }

  
    }

    public void DoAttack(int y)
    {
        Turn();

        Attacks selectedAttack;

        if(y >= currentPlayerAttacksArray.Length)
        {
            Debug.Log("Der Spieler hat versucht eine Attacke zu benutzen, die er nicht hat. >:( grrr");
            selectedAttack = Attacks.NULL;
        }
        else
        {
            selectedAttack = currentPlayerAttacksArray[y];
        }
        
        //Hier ALLE Attacen für NUR Spieler rein.
        switch (selectedAttack)
        {
            case Attacks.NULL:
                break;

             case Attacks.Protection:
                SetEffect(Statuseffekte.Geschützt, 1, true);
                break;

            case Attacks.Hammer:
                 currentopponentStats[0] -= Math.Max(0, 50 - currentopponentStats[2]); // 50 schaden / rüstung
                 Debug.Log("Hammer");
                break;


            case Attacks.Mutilation:
                currentopponentStats[0] -= Math.Max(0, 80 - currentopponentStats[2]);
                int random1 = Random.Range(0, 100);
                if (random1 <= 10)
                {
                SetEffect(Statuseffekte.Blutend, 3, false);
                }
                break;


             case Attacks.Strike:
                currentopponentStats[0] -= Math.Max(0, 35 - currentopponentStats[2]);
                int random2 = Random.Range(0, 100);
                if (random2 <= 45)
                {
                SetEffect(Statuseffekte.Gelähmt, 1, false);
                }
            break;

            case Attacks.PoisonDagger:
                currentopponentStats[0] -= Math.Max(0, 15 - currentopponentStats[2]);
                SetEffect(Statuseffekte.Vergiftet, 3, false);
                int random3 = Random.Range(0, 100);
                if (random3 <= 15)
                {
                 SetEffect(Statuseffekte.Blutend, 3, false);
                 }
                break;

            case Attacks.Fireball:
                currentopponentStats[0] -= Math.Max(0, 45 - currentopponentStats[2]);
                SetEffect(Statuseffekte.Brennend, 1, false);
            break;

            case Attacks.Dampen:
                currentopponentStats[1] = currentopponentStats[1] * (9/10);
            break; 

            case Attacks.Vengeance:
                int random4 = Random.Range(30, 100);
                currentopponentStats[0] -= Math.Max(0, random4 - currentopponentStats[2]);
                break;

            case Attacks.Dig:
                currentopponentStats[0] -= 10; // ignoriert rüstung
             break;

             case Attacks.LightOfHope:
                SetEffect(Statuseffekte.Hoffnungsvoll, 1, true);
            break;

            case Attacks.RedeemingStrike:
                if(currentOpponentEffects.ContainsKey(Statuseffekte.Vergiftet))
                {
                    currentopponentStats[0] -= Math.Max(0, 85 - currentopponentStats[2]);
                    currentOpponentEffects.Remove(Statuseffekte.Vergiftet);
                } else {
                currentopponentStats[0] -= Math.Max(0, 60 - currentopponentStats[2]);
                }
            break;

            case Attacks.Cleansing:
                        currentPlayerEffects.Clear();
            break;

            case Attacks.AgonyStrike:
                //if (currentplayerStats[0] * (8/10) > 0)
                //{
                currentplayerStats[0] = Math.Max(1, currentplayerStats[0] * 8 / 10);
                currentopponentStats[0] -= Math.Max(0, 90 - currentopponentStats[2]);
                //}
                //else Debug.Log("bitte nicht");
            break;

            case Attacks.Enlightenment:
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
        Gegner enemy = model.GetCurrentOponent();
        if (currentopponentStats[0] <= 0)
        {
            DoSave();
            if(enemy == Gegner.Endboss)
            {
                //Endboss besiegt, Spiel beenden
                Debug.Log("Endboss besiegt, Spiel beenden");
                //game stuff delete
                Application.Quit();   
            }
            SceneManager.UnloadSceneAsync("Fight");
            
        }else{
        Attacks i;
        //Hier timer, der bissl stallt.

        //hier entscheiden welche Attacke gew�hlt wird.
        int r = Random.Range(0, 10);
        if(enemy == Gegner.MiniBoss)
        {
            if(Random.Range(0, 3) == 3)
            {
                r = 9;
            }
        } else if(enemy == Gegner.Endboss)
            {
                if(r == 3 || r==4||r==5)
                {
                    r = 6;
                } else if(Random.Range(0,2) ==2)
                {
                    r = 0;
                }
            }

        
            switch (r)
            {
                    case 1:
                        i = Attacks.BasicAttack;
                        break;

                    case 2:
                        i = Attacks.BasicAttack;
                        break;
                    case 3:
                        i = Attacks.MinorAttack;
                        break;
                    case 4:
                        i = Attacks.MinorAttack;
                        break;
                    case 5:
                        i = Attacks.MinorAttack;
                        break;
                    case 6:
                        //i = Attacks.BuffSteal; //sollte nur möglich sein, wenn der Spieler einen Buff hat!!!
                        //habs so gemacht, liebe grüße DS
                        if (currentPlayerEffects.Keys.Any(IstBuff))
                        {
                            i = Attacks.BuffSteal;
                        }
                        else
                        {
                            i = Attacks.BasicAttack;
                        }
                        break;
                    case 7:
                        i = Attacks.AttackBlock;
                        break;
                    case 8:
                        i = Attacks.BasicAttack;
                        break;
                    case 9:
                        i = Attacks.Debuff;
                        break;
                    case 10:
                        i = Attacks.Debuff;
                        break;
                    case 0:
                        i = Attacks.Debuff;
                        break;
                    default:
                        i = Attacks.NULL;
                        break;
            }
            Debug.Log("Attacks wird ausgeführt " + i);

        
            //Hier ALLE Attacken für NUR jeden Gegner.
            switch (i)
            {
                case Attacks.NULL:
                    break;

                case Attacks.BasicAttack:
                    currentplayerStats[0] -= Math.Max(0, 40 - currentplayerStats[2]);
                    OponentFeedbackText.text = "Enemy uses " + enemyFeedbackTexts[((int)enemy-1)*2];
                    break;

                case Attacks.MinorAttack:
                    currentplayerStats[0] -= Math.Max(0, 30 - currentplayerStats[2]);
                OponentFeedbackText.text = "Enemy uses " + enemyFeedbackTexts[((int)enemy-1)*2+1];
                break;

                case Attacks.Debuff:
                   if(enemy == Gegner.MonsterPainting || enemy == Gegner.PrisonGuard)
                   {
                        SetEffect(Statuseffekte.Brennend, 3, true);
                        OponentFeedbackText.text = "Enemy set you in flames";
                   } else if(enemy == Gegner.ShadowEnemy)
                   {
                        SetEffect(Statuseffekte.Blutend, 2, true);
                        SetEffect(Statuseffekte.Gelähmt, 1, true);
                        OponentFeedbackText.text = "Enemy stunned you. You are bleeding";
                   }else if(enemy == Gegner.Insects) 
                   {
                        SetEffect(Statuseffekte.Vergiftet, 4, true);
                        OponentFeedbackText.text = "Enemy sprayed poison on you";

                   } else if(enemy == Gegner.MiniBoss ||enemy == Gegner.Endboss || enemy == Gegner.MiniBoss)
                   {
                        SetEffect(Statuseffekte.Verflucht, 9999, true);
                        OponentFeedbackText.text = "Enemy cursed you";
                   } else
                    {
                    currentplayerStats[0] -= 10; //Math.Max(0, 10 - currentopponentStats[2]);
                    OponentFeedbackText.text = "Enemy uses " + enemyFeedbackTexts[(int)enemy*2+1];
                    }

                    break;


                case Attacks.BuffSteal:
                    foreach (var effekt in currentPlayerEffects.Keys)
                    {
                        if (IstBuff(effekt))
                        {
                        currentOpponentEffects[effekt] = currentPlayerEffects[effekt];
                        }
                    }
                OponentFeedbackText.text = "Enemy removed you buff";
                break;

                case Attacks.AttackBlock:
                    SetEffect(Statuseffekte.Geschützt, 1, false);
                OponentFeedbackText.text = "Enemy BLOCKED";
                break;
                

            }
        opponentFeedbackPanel.SetActive(true);
        timer = 100;
        playerturn = true;
        bM.TurnChange(true);
        }
    }

    
    public void DoUseItem(Items item)
    {
        Turn();

        if(currentPlayerItems.ContainsKey(item) == false)
        {
            Debug.Log("Der Spieler hat ein Item benutzt, welches nicht im Inventar ist. >:( grrr");
            OponentTurn();
            return;
        }
        int randInt = Random.Range(0, 10);
        switch (item)
        {
            case Items.NULL:
                break;

            case Items.MagicApple:
                currentplayerStats[2] += 5;
            break;

            case Items.Beer:
                //wütend
                SetEffect(Statuseffekte.Wütend, 2, true);                //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN


                //30% Chance auf vergifted

                if (randInt <= 3)
                {
                    SetEffect(Statuseffekte.Vergiftet, 2, true);         //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN
                }
                break;

            case Items.PoisonMolotov:
                SetEffect(Statuseffekte.Vergiftet, 2, false);            //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN

                //20% Chance auf wütend
                if (randInt <= 2)
                {
                    SetEffect(Statuseffekte.Wütend, 2, false);           //numbers are WRONG! I JUST PUT EVERYWHERE 2 BECAUSE I CAN
                }
                break;

            case Items.HealingPotion:
                currentplayerStats[0] += 40;
                if (currentplayerStats[0] >= 101)
                {
                    currentplayerStats[0] = 100;
                }
                break;

            case Items.GreaterHealingPotion:
                currentplayerStats[0] += 70;
                if (currentplayerStats[0] >= 101)
                {
                    currentplayerStats[0] = 100;
                }
                break;

            case Items.HolyCross:
                SetEffect(Statuseffekte.Gesegnet, 999, true);
                break;

            case Items.PhoenixFeather:
                if (currentPlayerEffects.ContainsKey(Statuseffekte.Verflucht))
                {
                    SetEffect(Statuseffekte.Verflucht, 0, true);
                }
                break;

            case Items.Coins:
                currentopponentStats[0] -= 1;
                coinUsed = true;
                break;

            case Items.MirrorShard:
                currentopponentStats[0] -= GegnerDamageLastRound;
                break;

            case Items.Brick:
                currentopponentStats[0] -= Math.Max(0, 40 - currentopponentStats[2]);
                SetEffect(Statuseffekte.Gelähmt, 1, false);
                break;

        }
            currentPlayerItems[item] -= 1;
        if (currentPlayerItems[item] == 0)
        {
            currentPlayerItems.Remove(item);
        }
        bM.CheckItems();
        if(coinUsed == false)
        {
            OponentTurn();
            coinUsed = false;
        } else
        {
            playerturn = true;
            bM.TurnChange(true);
            coinUsed = false;
            
        }
    }

     private bool IstBuff(Statuseffekte effekt)
    {
        switch (effekt)
        {
            case Statuseffekte.Gesegnet: 
            case Statuseffekte.Geschützt:
                return true;

            default:
                return false;
        }
    }

    public Dictionary<Items, int> GiveCurrentPlayerItems()
    {
        return currentPlayerItems;
    }

    // Die Funktion Analyse:
    public void Analyse()
    { 
        analysisPanel.SetActive(true);
        switch (model.GetCurrentOponent())
        {
            case Gegner.PrisonGuard:
                AnalyseText.text = "This is the Prison Guard. He Has hight Health, but middle Armour!";
                break;

            case Gegner.StorageGuard:
                AnalyseText.text = "This is a simple Guard. He has litte HEalth, but a tuff Armour!";
                break;
            case Gegner.Insects:
                AnalyseText.text = "These little Beasts, with little to no health or Armour, can be a real Nightmare!";
                break;
            case Gegner.MonsterPainting:
                AnalyseText.text = "Just a Painting. (middle Health and Armour)";
                break;
            case Gegner.Endboss:
                AnalyseText.text = "HE CAN SEE YOU . . .";
                break;
            case Gegner.MiniBoss:
                AnalyseText.text = "This fella protects the Foyer. He has high health, but no armour.";
                break;
            case Gegner.ShadowEnemy:
                AnalyseText.text = "This Guy, with low healh, but middle Armour, can barely be seen.";
                break;
        }
        //timer = 300;
    }
}
