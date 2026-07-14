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


public class GM : MonoBehaviour
{
    private Model model;
    private Controller controller;
    private ButtonManager bM;


    //Für die Funktionalität der Coin
    //private bool coinUsed = false;



    //private int timer;

    [SerializeField] private GameObject SliderPlayerLife;
    [SerializeField] private GameObject SliderGegnerLife;
    [SerializeField] private GameObject SliderGegnerTime;

    [SerializeField] private Text OponentFeedbackText;
    [SerializeField] private Text AnalyseText;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private Sprite[] enemySprites;
    [SerializeField] private GameObject analysisPanel;
    [SerializeField] private GameObject opponentFeedbackPanel;
    [SerializeField] private OnHitEffect playerOnHitEffect;
    [SerializeField] private OnHitEffect enemyOnHitEffect;

    //private GM_Game gameMaster;

    //new Fight
    private List<Attacks> fokusedAttackslocal = new List<Attacks> { };
    private List<Attacks> unfokusedAttackslocal = new List<Attacks> { };
    private List<Attacks> devilArts = new List<Attacks> { Attacks.TheEnd, Attacks.DemonSword, Attacks.BlackFlash };
    [SerializeField] private Dictionary<Items, int> currentPlayerItems;

    private float timerGegner = 0.8f;
    private Attacks currentGegnerAttacks;
    private int attackDamage = 1;
    [SerializeField] private GameObject blockadePrefab;
    [SerializeField] private Sprite[] blockadeSprites;
    [SerializeField] private RectTransform[] playerButtonPositions;
    private List<GameObject> activeBlockades = new List<GameObject>();



    //Dieser Wert wird nur für Items benutzt
    private float timerGegnerSetTime = 4; //timerGegnerTickSpeed = 1; MUSS ZU tickspeed geändert werden, aus Gründen die du bei DS nachfragen kannst
    private int timerGegnerAnzahlZüge = -1;


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
        SetEnemyAttacks(model.GetCurrentOponent());
        enemyRenderer.sprite = enemySprites[(int)model.GetCurrentOponent() - 1];

        SliderGegnerLife.GetComponent<Slider>().maxValue = gegnerHealthlocal;

    }
    // Update is called once per frame
    void Update()
    {
        SliderPlayerLife.GetComponent<Slider>().value = playerHealthlocal;
        SliderGegnerLife.GetComponent<Slider>().value = gegnerHealthlocal;
        SliderGegnerTime.GetComponent<Slider>().value = timerGegner;

        if (playerHealthlocal <= 0)
        {
            if (currentPlayerItems.ContainsKey(Items.PhoenixFeather))
            {
                playerHealthlocal = 2;
            }
            else
            {
                DoSave();
                controller.SetPlayerHealth(5); //leben werden auf maximum gesetzt
                model.Save();
                SceneManager.LoadScene(0);
            }

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
            if (timerGegnerAnzahlZüge >= 0)
            {
                timerGegnerAnzahlZüge--;
            }
            else
            {
                timerGegnerSetTime = 4;
                timerGegnerAnzahlZüge = -1;
            }
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
                fokusedAttackslocal = new()
    {
        Attacks.HorAttack,
        Attacks.ArThrust,
        Attacks.BodyThrow,
        Attacks.Stomp
    };
                unfokusedAttackslocal = new()
    {
        Attacks.MagneticBurst,
        Attacks.FeintAttack,
        Attacks.RockThrow,
        Attacks.EyeBeam
    };
                break;

            case Gegner.MonsterPainting:
                fokusedAttackslocal = new()
    {
        Attacks.InfernoStrike,
        Attacks.FieryHead,
        Attacks.FlameBody,
        Attacks.HeadRush,
        Attacks.SkullTwist,
        Attacks.RagingPhoenix
    };
                unfokusedAttackslocal = new()
    {
        Attacks.Ignition,
        Attacks.FurnaceOfSouls,
        Attacks.CruelSun,
        Attacks.FireLight,
        Attacks.FlameCannon,
        Attacks.MagmaShot
    };
                break;

            case Gegner.ShadowEnemy:
                fokusedAttackslocal = new()
    {
        Attacks.VoidEdge,
        Attacks.DarkTouch,
        Attacks.UnstoppableBlow,
        Attacks.TigerClaw,
        Attacks.PhantomStep,
        Attacks.Consume,
        Attacks.UmbralAmbush,
        Attacks.PhantomSpear
    };
                unfokusedAttackslocal = new()
    {
        Attacks.DarkSiphon,
        Attacks.ReignOfDarkness,
        Attacks.ShadeSurge,
        Attacks.UmbralPrison,
        Attacks.Nightfall,
        Attacks.SoulRend
    };
                break;

            case Gegner.Insects:
                fokusedAttackslocal = new()
    {
        Attacks.HellishBite,
        Attacks.QuickStrike,
        Attacks.NecroticVenom,
        Attacks.DemonMandibles,
        Attacks.Lunge,
        Attacks.Sting
    };
                unfokusedAttackslocal = new()
    {
        Attacks.Glare,
        Attacks.WebSling,
        Attacks.VenomCrawl,
        Attacks.UroborosDNA,
        Attacks.SoulToxin,
        Attacks.AcidSpew
    };
                break;

            case Gegner.PrisonGuard:
                fokusedAttackslocal = new()
    {
        Attacks.VolcanicSlam,
        Attacks.MeltingGrasp,
        Attacks.FlameSkewer,
        Attacks.MoltenCrusher,
        Attacks.DevilTrigger,
        Attacks.BlazeKick,
        Attacks.BurningStrike
    };
                unfokusedAttackslocal = new()
    {
        Attacks.InfernalSurge,
        Attacks.FireCircle,
        Attacks.Vortex,
        Attacks.SolarFlare,
        Attacks.Ignite,
        Attacks.BlazingDomain,
        Attacks.HellfireBurst,
        Attacks.Overheat,
        Attacks.LavaGeyser
    };
                break;

            case Gegner.MiniBoss:
                fokusedAttackslocal = new()
    {
        Attacks.IllusionarySword,
        Attacks.GravityThrust,
        Attacks.PsychicMaw,
        Attacks.FalseReality,
        Attacks.ForceCrush,
        Attacks.Eclipse,
        Attacks.TranscendentFlow,
        Attacks.MindbladeSlash,
        Attacks.PsionicClaw
    };
                unfokusedAttackslocal = new()
    {
        Attacks.ThoughtLance,
        Attacks.Psychokinesis,
        Attacks.Brainshock,
        Attacks.Willbreaker,
        Attacks.FracturedConsciousness,
        Attacks.MindCrush,
        Attacks.PhantasmaWave,
        Attacks.DrainBeam,
        Attacks.TelepathicScream,
        Attacks.NeuralOverload,
        Attacks.EmeraldSplash
    };
                break;

            case Gegner.Endboss:
                fokusedAttackslocal = new()
    {
        Attacks.GripOfTheAbyss,
        Attacks.DeathTouch,
        Attacks.RedPhantom,
        Attacks.ArcaneStrike,
        Attacks.VoidExplosion,
        Attacks.Annihilation,
        Attacks.NightmareCrack,
        Attacks.DevilRush,
        Attacks.Oblivion,
        Attacks.TheHollowKing,
        Attacks.SeveredGrace,
        Attacks.Sinbreaker
    };
                unfokusedAttackslocal = new()
    {
        Attacks.HollowEcho,
        Attacks.AbyssalGlare,
        Attacks.DimensionShift,
        Attacks.FrenzyShadow,
        Attacks.SoulFire,
        Attacks.DevilOrbs,
        Attacks.NightmareEye,
        Attacks.LifeDrain,
        Attacks.SoulEruption,
        Attacks.Cataclysm,
        Attacks.StarFire,
        Attacks.DemonShade,
        Attacks.EndlessNight,
        Attacks.BloodOath,
        Attacks.AshesOfCreation
    };
                break;
            default:
                fokusedAttackslocal = new()
                {
                    Attacks.Punch
                };
                unfokusedAttackslocal = new()
                {
                    Attacks.FireBall
                };
                break;
        }
    }


    public void Counter()
    {
        bM.EnableButton(false);
        StartCoroutine(CounterRoutine());
    }
    public void Strike()
    {
        bM.EnableButton(false);
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
        bM.EnableButton(true);
        UnblockButtons();
        timerGegner = timerGegnerSetTime;
        if (Random.value < 0.2f)
        {
            if(Random.Value < 0.5f)
            {
                BlockButton(playerButtonPositions);
            }
            else
            {
                ShuffleButtons();
            }
            
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

    public void ShuffleButtons()
    {
        // save current position
        Vector2[] positions = new Vector2[3];

        for (int i = 0; i < 3; i++)
        {
            positions[i] = playerButtonPositions[i].anchoredPosition;
        }

        // idk Fisher-Yates methode frag net
        for (int i = 2; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Vector2 temp = positions[i];
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // set new position
        for (int i = 0; i < 3; i++)
        {
            playerButtonPositions[i].anchoredPosition = positions[i];
        }
    }


    //Falls der Spieler ein Item benutzt, wird diese Methode aufgerufen.

    public void DoUseItem(Items item)
    {

        if (currentPlayerItems.ContainsKey(item) == false)
        {
            Debug.Log("Der Spieler hat ein Item benutzt, welches nicht im Inventar ist. >:( grrr");
            return;
        }
        switch (item)
        {
            case Items.NULL:
                break;

            case Items.MagicApple:  //small rework, all MagicApples will be used maybe that you got?
                playerHealthlocal += 1;
                break;

            case Items.HealingPotion:
                playerHealthlocal += 2;
                break;

            case Items.GreaterHealingPotion:
                playerHealthlocal += 5;

                break;

            case Items.HolyCross:
                if (devilArts.Contains(currentGegnerAttacks))
                {
                    currentGegnerAttacks = Attacks.NULL;
                }
                playerHealthlocal++;
                break;

            case Items.Beer:
                //wütend
                timerGegnerSetTime = 6;
                timerGegnerAnzahlZüge = 3;
                break;

            case Items.PoisonMolotov:
                gegnerHealthlocal--;
                timerGegnerSetTime = 6;
                timerGegnerAnzahlZüge = 1;
                break;

            case Items.Scroll:          ///useless, nobody will ever use it -> rework
                playerHealthlocal -= 2;
                timerGegnerSetTime = 9;
                timerGegnerAnzahlZüge = 1;
                break;

            case Items.EvilScroll:
                playerHealthlocal--;
                gegnerHealthlocal -= 3;
                break;

            case Items.Bomb:
                gegnerHealthlocal -= 3;
                break;

            case Items.Coins:
                gegnerHealthlocal--;
                break;

            case Items.Lighter:
                timerGegnerSetTime = 2.8f;
                timerGegnerAnzahlZüge = 1;
                gegnerHealthlocal -= 2;
                break;

            case Items.RitualSword:
                //Devil Arts block + Gegnerlangsamer
                if (devilArts.Contains(currentGegnerAttacks))
                {
                    currentGegnerAttacks = Attacks.NULL;
                }
                timerGegnerSetTime = 6;
                timerGegnerAnzahlZüge = 1;
                break;

            case Items.MirrorShard:
                //Devil Arts block + -GegnerLeben
                if (devilArts.Contains(currentGegnerAttacks))
                {
                    currentGegnerAttacks = Attacks.NULL;
                }
                gegnerHealthlocal--;
                break;

            case Items.Brick:
                gegnerHealthlocal -= 2;
                break;

            case Items.StrangeKey:
                DoSave();
                model.Save();
                SceneManager.LoadScene(0);
                break;

            case Items.Shovel:
                gegnerHealthlocal -= 1;
                timerGegnerSetTime = 8;
                timerGegnerAnzahlZüge = 1;
                break;

            case Items.FishingRod:
                if (!devilArts.Contains(currentGegnerAttacks))
                {
                    currentGegnerAttacks = Attacks.NULL;
                }
                break;

        }
        if (item != Items.Lighter || item != Items.RitualSword || item != Items.StrangeKey || item != Items.FishingRod)
        {
            currentPlayerItems[item] -= 1;
            if (currentPlayerItems[item] == 0)
            {
                currentPlayerItems.Remove(item);
            }
        }

        bM.CheckItems();
        timerGegner = 0;

        bM.EnableButton(false);
    }





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

    public void SetGegnerTime(float time)
    {
        timerGegner = time;
    }


}
