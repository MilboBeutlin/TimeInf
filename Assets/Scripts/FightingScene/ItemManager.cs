using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

// Applies the effect of a used item (healing, damage, timer manipulation, devil art blocking, etc.)
// and removes it from the player's inventory once used if it isn't a required puzzle item
public class ItemManager : MonoBehaviour
{
    private GM gm;
    private EnemyManager enemyManager;
    private FightLogic fightLogic;
    private ButtonManager bM;

    [SerializeField] private Dictionary<Items, int> currentPlayerItems;

    public Dictionary<Items, int> CurrentPlayerItems
    {
        get => currentPlayerItems;
        set => currentPlayerItems = value;
    }

    void Awake()
    {
        gm = GetComponent<GM>();
        enemyManager = GetComponent<EnemyManager>();
        fightLogic = GetComponent<FightLogic>();
    }

    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
    }

    // Called when the player uses an item.
    public void DoUseItem(Items item)
    {
        if (currentPlayerItems.ContainsKey(item) == false)
        {
            return;
        }

        StartCoroutine(DoUseItemRoutine(item));
    }

    private IEnumerator DoUseItemRoutine(Items item)
    {
        bool damagedEnemy = false;

        switch (item)
        {
            case Items.NULL:
                break;

            case Items.MagicApple://consumes all MagicApples till  you got 5HP, each gives you one

                int healAmount = Math.Min(currentPlayerItems[item], 5 - gm.PlayerHealth);

                gm.PlayerHealth += healAmount;
                currentPlayerItems[item] -= healAmount;
                break;

            case Items.HealingPotion:
                gm.PlayerHealth += 2;
                currentPlayerItems[item] -= 1;
                break;

            case Items.GreaterHealingPotion:
                gm.PlayerHealth += 5;
                currentPlayerItems[item] -= 1;
                break;

            case Items.HolyCross:
                if (enemyManager.DevilArts.Contains(fightLogic.CurrentGegnerAttack))
                {
                    fightLogic.CurrentGegnerAttack = Attacks.NULL;
                }
                gm.PlayerHealth++;
                currentPlayerItems[item] -= 1;
                break;

            case Items.Beer:
                fightLogic.TimerGegnerTickSpeed = 4f / 6f;
                fightLogic.TimerGegnerAnzahlZüge = 3;
                currentPlayerItems[item] -= 1;
                break;

            case Items.PoisonMolotov:
                gm.GegnerHealth--;
                damagedEnemy = true;
                fightLogic.TimerGegnerTickSpeed = 4f / 6f;
                fightLogic.TimerGegnerAnzahlZüge = 1;
                currentPlayerItems[item] -= 1;
                break;

            case Items.Scroll:
                gm.PlayerHealth++;
                fightLogic.TimerGegnerTickSpeed = 4f / 9f;
                fightLogic.TimerGegnerAnzahlZüge = 1;
                currentPlayerItems[item] -= 1;
                break;

            case Items.EvilScroll:
                gm.PlayerHealth--;
                gm.GegnerHealth -= 3;
                damagedEnemy = true;
                currentPlayerItems[item] -= 1;
                break;

            case Items.Bomb:
                gm.GegnerHealth -= 3;
                damagedEnemy = true;
                currentPlayerItems[item] -= 1;
                break;

            case Items.Coins:
                gm.GegnerHealth--;
                damagedEnemy = true;
                currentPlayerItems[item] -= 1;
                break;

            case Items.Lighter:
                fightLogic.TimerGegnerTickSpeed = 4f / 2.8f;
                fightLogic.TimerGegnerAnzahlZüge = 1;
                gm.GegnerHealth -= 2;
                damagedEnemy = true;
                break;

            case Items.RitualSword:
                if (enemyManager.DevilArts.Contains(fightLogic.CurrentGegnerAttack))
                {
                    fightLogic.CurrentGegnerAttack = Attacks.NULL;
                }
                fightLogic.TimerGegnerTickSpeed = 4f / 6f;
                fightLogic.TimerGegnerAnzahlZüge = 1;
                break;

            case Items.MirrorShard:
                if (enemyManager.DevilArts.Contains(fightLogic.CurrentGegnerAttack))
                {
                    fightLogic.CurrentGegnerAttack = Attacks.NULL;
                }
                gm.GegnerHealth--;
                damagedEnemy = true;
                currentPlayerItems[item] -= 1;
                break;

            case Items.Brick:
                gm.GegnerHealth -= 2;
                damagedEnemy = true;
                currentPlayerItems[item] -= 1;
                break;

            case Items.StrangeKey:
                gm.DoSave();
                gm.Model.Save();
                SceneManager.LoadScene(0);
                break;

            case Items.Shovel:
                gm.GegnerHealth -= 1;
                damagedEnemy = true;
                fightLogic.TimerGegnerTickSpeed = 4f / 8f;
                fightLogic.TimerGegnerAnzahlZüge = 1;
                currentPlayerItems[item] -= 1;
                break;

            case Items.FishingRod:
                if (!enemyManager.DevilArts.Contains(fightLogic.CurrentGegnerAttack))
                {
                    fightLogic.CurrentGegnerAttack = Attacks.NULL;
                }
                break;
        }

        // Wait here until the hit animation is finished before continuing
        if (damagedEnemy)
        {
            yield return StartCoroutine(gm.EnemyOnHitEffect.PlayHitEffect());
        }

        if (currentPlayerItems[item] <= 0)
        {
            currentPlayerItems.Remove(item);
        }

        bM.CheckItems();
        fightLogic.TimerGegner = 0;

        bM.EnableButton(false);
    }

    public Dictionary<Items, int> giveCurrentPlayerItems()
    {
        return currentPlayerItems;
    }
}