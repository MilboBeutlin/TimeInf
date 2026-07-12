using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using TMPro;
using System.Threading.Tasks;
using System.Collections;


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
    private string[] enemyFeedbackTexts = new string[] { "Eye beam", "Horn attack", "Flaming strike", "Heabutt", "Void edge", "Shadow touch", "Hellish Bite", "Leg lunge", "Volcanic Slam", "Magma Burst", "Eclipse", "Phantasma wave", "CrownOfDamnation", "Chaos Lance" };
    [SerializeField] private OnHitEffect playerOnHitEffect;
    [SerializeField] private OnHitEffect enemyOnHitEffect;

    //private GM_Game gameMaster;

    //new Fight
    private List<Attacks> fokusedAttackslocal = new List<Attacks> { Attacks.Schlag };
    private List<Attacks> unfokusedAttackslocal = new List<Attacks> { Attacks.Feuerball };
    private List<Attacks> devilArts = new List<Attacks> { Attacks.TheEnd, Attacks.DemonSword, Attacks.BlackFlash };
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;

    private float timerGegner = 0.8f;
    private Attacks currentGegnerAttacks;
    private int attackDamage = 1;
    [SerializeField] private GameObject blockadePrefab;
    [SerializeField] private Sprite[] blockadeSprites;
    [SerializeField] private Transform[] playerButtonPositions;
    private List<GameObject> activeBlockades = new List<GameObject>();


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
        //SetEnemyAttacks(model.GetCurrentOponent()); to set the attacks as soon as i get the names from leon
        enemyRenderer.sprite = enemySprites[(int)model.GetCurrentOponent() - 1];

        SliderGegnerLife.GetComponent<Slider>().maxValue = gegnerHealthlocal;
    }
    // Update is called once per frame
    void Update()
    {
        SliderPlayerLife.GetComponent<Slider>().value = playerHealthlocal;
        SliderGegnerLife.GetComponent<Slider>().value = gegnerHealthlocal;

        if (playerHealthlocal <= 0)
        {
            controller.SetPlayerHealth(5);
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

        if (timerGegner <= 0)
        {
            UnblockButtons();
            StartCoroutine(GegnerTurn());
        }
        else
        {
            timerGegner -= Time.deltaTime;
        }
    }

    //Läd alle relevanten Daten aus der DB in diese Klasse
    public void DoLoad()
    {
        Debug.Log("Fs geladen.");
        // Läd den rest
        currentPlayerItems = model.GetCurrentPlayerItems();

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
    private void SetEnemyAttacks(Gegner gegner)
    {
        switch (gegner)
        {
            case Gegner.StorageGuard:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
            case Gegner.MonsterPainting:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
            case Gegner.ShadowEnemy:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
            case Gegner.Insects:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
            case Gegner.PrisonGuard:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
            case Gegner.MiniBoss:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
            case Gegner.Endboss:
                fokusedAttackslocal = new() { };
                unfokusedAttackslocal = new() { };
                break;
        }
    }


    public void Counter()
    {
        StartCoroutine(CounterRoutine());
    }
    public void Strike()
    {
        StartCoroutine(StrikeRoutine());
    }

    private IEnumerator CounterRoutine()
    {
        Debug.Log("Counter");
        Debug.Log("Attack = " + currentGegnerAttacks);
        if (fokusedAttackslocal.Contains(currentGegnerAttacks))
        {
            gegnerHealthlocal--;
            EnemyFeedbackText("");
            yield return StartCoroutine(enemyOnHitEffect.PlayHitEffect());

        }
        else
        {
            playerHealthlocal -= attackDamage;
            EnemyFeedbackText("");
            yield return StartCoroutine(playerOnHitEffect.PlayHitEffect());
        }
        currentGegnerAttacks = Attacks.NULL;
        timerGegner = 0f;
    }

    private IEnumerator StrikeRoutine()
    {
        Debug.Log("Strike");
        Debug.Log("Attack = " + currentGegnerAttacks);
        if (unfokusedAttackslocal.Contains(currentGegnerAttacks))
        {
            gegnerHealthlocal--;
            EnemyFeedbackText("");
            yield return StartCoroutine(enemyOnHitEffect.PlayHitEffect());
        }
        else
        {
            playerHealthlocal -= attackDamage;
            EnemyFeedbackText("");
            yield return StartCoroutine(playerOnHitEffect.PlayHitEffect());
        }
        currentGegnerAttacks = Attacks.NULL;
        timerGegner = 0f;
    }


    public IEnumerator GegnerTurn()
    {
        timerGegner = 4f;
        if (Random.value < 0.2f)
        {
            BlockButton(playerButtonPositions);
        }

        //testen, ob mit der alten Attacke gedealt wurde
        if (currentGegnerAttacks != Attacks.NULL)
        {
            playerHealthlocal -= attackDamage;
            EnemyFeedbackText("");
            yield return StartCoroutine(playerOnHitEffect.PlayHitEffect());
            currentGegnerAttacks = Attacks.NULL;
        }

        //Attacke setzen
        float devilArtChance = GetDevilArtChance(model.GetCurrentOponent());

        if (Random.value < devilArtChance)
        {
            currentGegnerAttacks = devilArts[Random.Range(0, devilArts.Count)];
            attackDamage = 2;
            EnemyFeedbackText("<color=red>" + currentGegnerAttacks + "</color>");
        }
        else
        {
            if (Random.value < 0.5f)
            {
                currentGegnerAttacks = fokusedAttackslocal[Random.Range(0, fokusedAttackslocal.Count)];
            }
            else
            {
                currentGegnerAttacks = unfokusedAttackslocal[Random.Range(0, unfokusedAttackslocal.Count)];
            }

            attackDamage = 1;
            EnemyFeedbackText(currentGegnerAttacks.ToString());
        }
    }

    private float GetDevilArtChance(Gegner gegner)
    {
        return gegner switch
        {
            Gegner.ShadowEnemy => 0.1f,
            Gegner.MiniBoss => 0.15f,
            Gegner.Endboss => 0.25f,
            Gegner.Insects => 0f,
            _ => 0.05f
        };
    }

    public void BlockButton(Transform[] playerButtons)
    {
        foreach (Transform playerButton in playerButtons)
        {
            GameObject obj = Instantiate(blockadePrefab, playerButton);

            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            obj.transform.localScale = new Vector3(1.2f, 2f, 1f);

            obj.transform.SetAsLastSibling();

            obj.GetComponent<ButtonBlockade>().Setup(4, blockadeSprites);
            activeBlockades.Add(obj);
        }
    }
    private void UnblockButtons()
    {
        foreach (GameObject blockade in activeBlockades)
        {
            if (blockade != null)
                Destroy(blockade);
        }

        activeBlockades.Clear();
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
        if (OponentFeedbackText)
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
