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
    //private bool coinUsed = false;

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
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;

    private int timerGegner = 40;
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
        SliderPlayerLife.GetComponent<Slider>().value = playerHealthlocal;
        SliderGegnerLife.GetComponent<Slider>().value = gegnerHealthlocal;

        if (playerHealthlocal <= 0)
        {
            playerHealthlocal = 100;
            model.Save();
            SceneManager.LoadScene(0);
        }

        if (gegnerHealthlocal <= 0)
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
            model.LightsSwitchToFight(false);

        }


        //GegnerLogic

        if(timerGegner <= 0)
        {
            GegnerTurn();
        } else
        {
            timerGegner--;
        }
    }

    //Läd alle relevanten Daten aus der DB in diese Klasse
    public void DoLoad()
    {
        Debug.Log("Fs geladen.");
        // Läd den rest
        currentPlayerItems = model.GetCurrentPlayerItems();

        gegnerHealthlocal = model.GetCurrentOponnentStats();
        playerHealthlocal = model.GetPlayerHealth();

        gegnerHealthlocal = model.GetGegnerHealth();
    }

    // Speichert alle Relevanten Daten aus dieser Klasse in die DB

    public void DoSave()
    {

        controller.SetPlayerHealth(playerHealthlocal);
        controller.SetGegnerHealth(gegnerHealthlocal);
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


}
