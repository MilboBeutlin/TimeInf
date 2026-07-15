using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using System.Text;

// Handles the core fight loop: enemy turn timer, attack resolution (Counter/Strike),
// and the button blockade/shuffle mechanics used during enemy turns.
public class FightLogic : MonoBehaviour
{
    private GM gm;
    private EnemyManager enemyManager;
    private ButtonManager bM;

    private Attacks currentGegnerAttacks;
    private int attackDamage = 1;

    private float timerGegner = 0.8f;

    // Only used by items to speed up/slow down the enemy timer
    private float timerGegnerTickSpeed = 1f;
    private int timerGegnerAnzahlZüge = -1;

    [SerializeField] private GameObject blockadePrefab;
    [SerializeField] private Sprite[] blockadeSprites;
    [SerializeField] private RectTransform[] playerButtonPositions;
    private List<GameObject> activeBlockades = new List<GameObject>();

    public float TimerGegner
    {
        get => timerGegner;
        set => timerGegner = value;
    }

    public float TimerGegnerTickSpeed
    {
        get => timerGegnerTickSpeed;
        set => timerGegnerTickSpeed = value;
    }

    public int TimerGegnerAnzahlZüge
    {
        get => timerGegnerAnzahlZüge;
        set => timerGegnerAnzahlZüge = value;
    }

    public Attacks CurrentGegnerAttack
    {
        get => currentGegnerAttacks;
        set => currentGegnerAttacks = value;
    }

    void Awake()
    {
        gm = GetComponent<GM>();
        enemyManager = GetComponent<EnemyManager>();
    }

    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
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

    // Counter only beats focused attacks; any other attack still hits the player
    private IEnumerator CounterRoutine()
    {
        if (enemyManager.FokusedAttacks.Contains(currentGegnerAttacks))
        {
            gm.GegnerHealth--;
            gm.EnemyFeedbackText("");
            yield return StartCoroutine(gm.EnemyOnHitEffect.PlayHitEffect());
        }
        else
        {
            gm.PlayerHealth -= attackDamage;
            gm.EnemyFeedbackText("");
            yield return StartCoroutine(gm.PlayerOnHitEffect.PlayHitEffect());
        }
        currentGegnerAttacks = Attacks.NULL;
        timerGegner = 0f;
    }

    // Strike only beats unfocused attacks; any other attack still hits the player
    private IEnumerator StrikeRoutine()
    {
        if (enemyManager.UnfokusedAttacks.Contains(currentGegnerAttacks))
        {
            gm.GegnerHealth--;
            gm.EnemyFeedbackText("");
            yield return StartCoroutine(gm.EnemyOnHitEffect.PlayHitEffect());
        }
        else
        {
            gm.PlayerHealth -= attackDamage;
            gm.EnemyFeedbackText("");
            yield return StartCoroutine(gm.PlayerOnHitEffect.PlayHitEffect());
        }
        currentGegnerAttacks = Attacks.NULL;
        timerGegner = 0f;
    }

    // Resolves the previous attack if the player didn't react in time, then rolls a new
    // attack (devil art / focused / unfocused) and occasionally blocks or shuffles buttons
    public IEnumerator GegnerTurn()
    {
        bM.EnableButton(true);
        UnblockButtons();
        timerGegner = 4f;
        SpecialMove();

        if (currentGegnerAttacks != Attacks.NULL)
        {
            gm.PlayerHealth -= attackDamage;
            gm.EnemyFeedbackText("");
            yield return StartCoroutine(gm.PlayerOnHitEffect.PlayHitEffect());
            currentGegnerAttacks = Attacks.NULL;
        }

        float devilArtChance = enemyManager.GetDevilArtChance(gm.Model.GetCurrentOponent());

        if (Random.value < devilArtChance)
        {
            currentGegnerAttacks = enemyManager.DevilArts[Random.Range(0, enemyManager.DevilArts.Count)];
            attackDamage = 2;
            gm.EnemyFeedbackText("<color=red>" + FormatAttackName(currentGegnerAttacks) + "</color>");
        }
        else
        {
            if (Random.value < 0.5f)
            {
                currentGegnerAttacks = enemyManager.FokusedAttacks[Random.Range(0, enemyManager.FokusedAttacks.Count)];
            }
            else
            {
                currentGegnerAttacks = enemyManager.UnfokusedAttacks[Random.Range(0, enemyManager.UnfokusedAttacks.Count)];
            }

            attackDamage = 1;
            gm.EnemyFeedbackText(FormatAttackName(currentGegnerAttacks));
        }
    }

    //enemy uses ShuffleButtons or BlockButton with 20% based if he can do it or not
    public void SpecialMove()
    {
        int blockadeHP = enemyManager.GetBlockadeHP(gm.Model.GetCurrentOponent());
        bool canShuffle = enemyManager.CanShuffle(gm.Model.GetCurrentOponent());

        if (Random.value < 0.2f)
        {
            if (blockadeHP > 0 && canShuffle)
            {
                if (Random.value < 0.5f)
                {
                    BlockButton(playerButtonPositions, blockadeHP);
                }
                else
                {
                    ShuffleButtons();
                }
            }
            else if (blockadeHP > 0)
            {
                BlockButton(playerButtonPositions, blockadeHP);
            }
            else if (canShuffle)
            {
                ShuffleButtons();
            }
        }
    }
    //creates a blockade for each prefab that fits over the targeted button and
    //sets it as sibling so it is coverinng the button so it can't be pressed
    public void BlockButton(Transform[] playerButtons, int blockadeHP)
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

            obj.GetComponent<ButtonBlockade>().Setup(blockadeHP, blockadeSprites);
            activeBlockades.Add(obj);
        }
    }

    private void UnblockButtons()
    {
        foreach (GameObject blockade in activeBlockades)
        {
            if (blockade != null)
            {
                Destroy(blockade);
            }
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

        // Fisher-Yates shuffle
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

    // Converts an attack enum name into a readable string by putting spaces before uppercase letters.
    public static string FormatAttackName(Attacks attack)
    {
        string text = attack.ToString();
        StringBuilder sb = new();

        for (int i = 0; i < text.Length; i++)
        {
            if (i > 0 && char.IsUpper(text[i]))
                sb.Append(' ');

            sb.Append(text[i]);
        }

        return sb.ToString();
    }

    public void FreezeGegnerTimer()
    {
        timerGegnerTickSpeed = 0f;
    }
}
