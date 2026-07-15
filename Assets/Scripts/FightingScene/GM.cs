using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;


// Head game manager for the fight scene.
// Coordinates player/enemy health, UI sliders, saving/loading with Model & Controller.
// In EnemyManager, FightLogic & ItemMangaer is where the logic for enemy attacks, timers and items is located.
public class GM : MonoBehaviour
{
    private Model model;
    private Controller controller;
    private ButtonManager bM;

    private EnemyManager enemyManager;
    private FightLogic fightLogic;
    private ItemManager itemManager;

    [SerializeField] private GameObject SliderPlayerLife;
    [SerializeField] private GameObject SliderGegnerLife;
    [SerializeField] private GameObject SliderGegnerTime;

    [SerializeField] private Text OponentFeedbackText;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private Sprite[] enemySprites;
    [SerializeField] private GameObject analysisPanel;
    [SerializeField] private GameObject opponentFeedbackPanel;
    [SerializeField] private OnHitEffect playerOnHitEffect;
    [SerializeField] private OnHitEffect enemyOnHitEffect;

    private int playerHealthlocal;
    private int gegnerHealthlocal;

    // Public access for the other classes (FightLogic, ItemManager, EnemyManager)
    public Model Model => model;
    public Controller Controller => controller;
    public ButtonManager ButtonManagerRef => bM;
    public OnHitEffect PlayerOnHitEffect => playerOnHitEffect;
    public OnHitEffect EnemyOnHitEffect => enemyOnHitEffect;

    public int PlayerHealth
    {
        get => playerHealthlocal;
        set => playerHealthlocal = value;
    }

    public int GegnerHealth
    {
        get => gegnerHealthlocal;
        set => gegnerHealthlocal = value;
    }

    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();
        controller = FindAnyObjectByType<Controller>();

        enemyManager = GetComponent<EnemyManager>();
        fightLogic = GetComponent<FightLogic>();
        itemManager = GetComponent<ItemManager>();

        DoLoad();
        EnemyFeedbackText("");
        enemyManager.SetEnemyAttacks(model.GetCurrentOponent());
        enemyRenderer.sprite = enemySprites[(int)model.GetCurrentOponent() - 1];

        SliderGegnerLife.GetComponent<Slider>().maxValue = gegnerHealthlocal;
    }

    void Update()
    {
        SliderPlayerLife.GetComponent<Slider>().value = playerHealthlocal;
        SliderGegnerLife.GetComponent<Slider>().value = gegnerHealthlocal;
        SliderGegnerTime.GetComponent<Slider>().value = fightLogic.TimerGegner;

        // Player defeated: revive with Phoenix Feather if available, otherwise sent player back to MainMenu
        if (playerHealthlocal <= 0)
        {
            if (itemManager.CurrentPlayerItems.ContainsKey(Items.PhoenixFeather))
            {
                playerHealthlocal = 2;
            }
            else
            {
                DoSave();
                controller.SetPlayerHealth(5);
                model.Save();
                SceneManager.LoadScene(0);
            }
        }

        // Enemy defeated: end game if it was the final boss, else unload fight scene
        if (gegnerHealthlocal <= 0)
        {
            DoSave();
            if (model.GetCurrentOponent() == Gegner.Endboss)
            {
                controller.NewGame();
                SceneManager.LoadScene(3);
            }
            SceneManager.UnloadSceneAsync("Fight");
            model.LightsSwitchToFight(false);
        }

        // Enemy turn timer: counts down, tick speed can be changed
        if (fightLogic.TimerGegner <= 0)
        {
            if (fightLogic.TimerGegnerAnzahlZüge >= 0)
            {
                fightLogic.TimerGegnerAnzahlZüge--;
            }
            else
            {
                fightLogic.TimerGegnerTickSpeed = 1f;
                fightLogic.TimerGegnerAnzahlZüge = -1;
            }
            StartCoroutine(fightLogic.GegnerTurn());
        }
        else
        {
            fightLogic.TimerGegner -= Time.deltaTime * fightLogic.TimerGegnerTickSpeed;
        }
    }

    public void DoLoad()
    {
        itemManager.CurrentPlayerItems = model.GetCurrentPlayerItems();
        playerHealthlocal = model.GetPlayerHealth();
        gegnerHealthlocal = model.GetGegnerHealth();
    }

    public void DoSave()
    {
        controller.SetPlayerHealth(playerHealthlocal);
        controller.SetGegnerHealth(gegnerHealthlocal);
        controller.SetPlayerItems(itemManager.CurrentPlayerItems);
    }

    //Sets enemyFeedbackText based on the parameter, it also de/activates based if there is text or not
    public void EnemyFeedbackText(string enemyFeedbackText)
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

    //Forwarding methods are there so references to GM still work as before
    public void Counter()
    {
        fightLogic.Counter();
    }

    public void Strike()
    {
        fightLogic.Strike();
    }

    public void DoUseItem(Items item)
    {
        itemManager.DoUseItem(item);
    }

    public Dictionary<Items, int> giveCurrentPlayerItems()
    {
        return itemManager.giveCurrentPlayerItems();
    }
    public void FreezeGegnerTimer()
    {
        fightLogic.FreezeGegnerTimer();
    }
}
