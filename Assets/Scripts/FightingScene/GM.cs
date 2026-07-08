using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using TMPro;
using System.Threading.Tasks;

public class GM : MonoBehaviour
{
    private Model model;
    private Controller controller;
    private ButtonManager bM;

    //Für die Funktionalität der Coin
    private bool coinUsed = false;


    //Stats
    //player
    [SerializeField] private int[] currentplayerStats;
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;
    [SerializeField] private Dictionary<Statuseffekte, int> currentPlayerEffects;
    //gegner
    [SerializeField] private int[] currentopponentStats; //health, attack, armor, speed, dk
    [SerializeField] private Attacks[] currentOponnentAttacks;
    [SerializeField] private Dictionary<Statuseffekte, int> currentOpponentEffects;

    private int GegnerDamageLastRound; //nur relevant für Item Spiegelfragment!!

    //private int timer;

    [SerializeField] private GameObject SliderPlayerLife;
    [SerializeField] private GameObject SliderGegnerLife;

    [SerializeField] private Text OponentFeedbackText;
    [SerializeField] private Text AnalyseText;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private Sprite[] enemySprites;
    [SerializeField] private GameObject analysisPanel;
    [SerializeField] private GameObject opponentFeedbackPanel;
    private string[] enemyFeedbackTexts = new string[]{"Eye beam", "Horn attack","Flaming strike", "Heabutt", "Void edge", "Shadow touch", "Hellish Bite", "Leg lunge", "Volcanic Slam", "Magma Burst", "Eclipse", "Phantasma wave", "CrownOfDamnation", "Chaos Lance"};
    [SerializeField] private OnHitEffect player;
    [SerializeField] private OnHitEffect enemy;

    //private GM_Game gameMaster;

    //new Fight
    private List<Attacks> fokusedAttackslocal;
    private List<Attacks> unfokusedAttackslocal;

    private int timerGegner;
    private Attacks currentGegnerAttacks;
    

    private int playerHealthlocal;
    private int gegnerHealthlocal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();
        controller = FindAnyObjectByType<Controller>();

        DoLoad();
        EnemyFeedbackText("");
        enemyRenderer.sprite = enemySprites[(int)model.GetCurrentOponent() - 1];
    }
    // Update is called once per frame
    void Update()
    {
        SliderPlayerLife.GetComponent<Slider>().value = currentplayerStats[0];
        SliderGegnerLife.GetComponent<Slider>().value = currentopponentStats[0];

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

        if (currentopponentStats[0] <= 0)
        {
            DoSave();
            if (model.GetCurrentOponent() == Gegner.Endboss)
            {
                //Endboss besiegt, Spiel beenden
                Debug.Log("Endboss besiegt, Spiel beenden");
                //game stuff delete
                controller.NewGame();
                SceneManager.LoadScene(3);
            }
            SceneManager.UnloadSceneAsync("Fight");
            model.LightsSwitchtoFight(false);

        }


        //GegnerLogic

        if(timerGegner <= 0)
        {
            
        } else
        {
            timerGegner--;
        }
    }

    //Läd alle relevanten Daten aus der DB in diese Klasse
    public void DoLoad()
    {

        // Läd den rest
        currentPlayerItems = model.GetCurrentPlayerItems();
        currentopponentStats = model.GetCurrentOponnentStats();
        SliderGegnerLife.GetComponent<Slider>().maxValue = currentopponentStats[0];
    }

    // Speichert alle Relevanten Daten aus dieser Klasse in die DB

    public void DoSave()
    {

        
        controller.SetPlayerItems(currentPlayerItems);

        
    }



    public void Counter()
    {
        if(fokusedAttackslocal.Contains(currentGegnerAttacks))
        {
            gegnerHealthlocal--;
        } else
        {
            playerHealthlocal--;
        }
        currentGegnerAttacks = Attacks.NULL;
    }

    public void Strike()
    {
        if (unfokusedAttackslocal.Contains(currentGegnerAttacks))
        {
            gegnerHealthlocal--;
        }
        else
        {
            playerHealthlocal--;
        }
        currentGegnerAttacks = Attacks.NULL;
    }
    

    public void GegnerTurn()
    {
        //testen, ob mit der alten Attacke gedealt wurde
        if (currentGegnerAttacks != Attacks.NULL)
        {
            playerHealthlocal--;
            currentGegnerAttacks = Attacks.NULL;
        }

        //Attacke setzen
        if (Random.Range(0, 1) == 1)
        {
            currentGegnerAttacks = fokusedAttackslocal[Random.Range(0, fokusedAttackslocal.Count())];
        }
        else
        {
            currentGegnerAttacks = unfokusedAttackslocal[Random.Range(0, unfokusedAttackslocal.Count())];
        }

        EnemyFeedbackText(currentGegnerAttacks.ToString());

        timerGegner = 40;
    }

    //Falls der Spieler ein Item benutzt, wird diese Methode aufgerufen.
    /*
    public async Task DoUseItem(Items item)
    {
        

        if(currentPlayerItems.ContainsKey(item) == false)
        {
            Debug.Log("Der Spieler hat ein Item benutzt, welches nicht im Inventar ist. >:( grrr");
            await OponentTurn();
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
                await enemy.PlayHitEffect();
                coinUsed = true;
                break;

            case Items.MirrorShard:
                currentopponentStats[0] -= GegnerDamageLastRound;
                await enemy.PlayHitEffect();
                break;

            case Items.Brick:
                currentopponentStats[0] -= Math.Max(0, 40 - currentopponentStats[2]);
                SetEffect(Statuseffekte.Gelähmt, 1, false);
                await enemy.PlayHitEffect();
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
            await Task.Delay(600);
            await OponentTurn();
            coinUsed = false;
        } else
        {
            
            bM.TurnChange(true);
            coinUsed = false;
            
        }
    }

    */

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
                AnalyseText.text = "This is a simple Guard. He has little HEalth, but a tuff Armour!";
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
    }

    private void EnemyFeedbackText(string enemyFeedbackText)
    {
        if(OponentFeedbackText)
        {
            OponentFeedbackText.text = enemyFeedbackText;
        }

        if (opponentFeedbackPanel && enemyFeedbackText != null && enemyFeedbackText.Length > 0)
        {
            opponentFeedbackPanel.SetActive(true);
        }
        else if (opponentFeedbackPanel && (enemyFeedbackText == null || enemyFeedbackText.Length == 0))
        {
            opponentFeedbackPanel.SetActive(false);
        }
    }

    // Werte setzten / Verändern



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

}
