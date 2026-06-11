using UnityEngine;
using System.Collections.Generic;

public class Datenbank : MonoBehaviour
{
    private Model model;

    //Alle Stats im Game
    //Player:
    private Attacks[] currentPlayerAttacks;
    //private Statuseffekte[] currentPlayerEffects;
    private Dictionary<Statuseffekte, int> playerEffects = new Dictionary<Statuseffekte, int>();

    [SerializeField] private Dictionary<Items, int> playerItems = new Dictionary<Items, int>();
    private int[] currentPlayerStats = new int[]{100,25,20,6,0}; //health, attack, armor, speed, dk

    //Gegner:
    private Gegner currentOponent;
    private Attacks[] currentOponnentAttacks;
    private Dictionary<Statuseffekte, int> oponentEffects = new Dictionary<Statuseffekte, int>();
    private int[] currentOponnentStats; //health, attack, armor, speed, dk

    private void Start()
    {
        model = FindAnyObjectByType<Model>();
        //Alle stats im Game
        currentPlayerAttacks = new Attacks[6];
        for (int i = 0; i < currentPlayerAttacks.Length; i++)
        {
            currentPlayerAttacks[i] = Attacks.NULL;
        }

        currentOponnentAttacks = new Attacks[6];
        for (int i = 0; i <currentOponnentAttacks.Length; i++)
        {
            currentOponnentAttacks[i] = Attacks.NULL;   
        }
    
    }
    /*private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }*/
    
    //Alle Satas im Game
    //player
    public Attacks[] GetCurrentPlayerAttacks() {
        return currentPlayerAttacks;
    }

    public void SetCurrentPlayerAttacks(Attacks[] currentPlayerAttacks) {
        this.currentPlayerAttacks = currentPlayerAttacks;
    }

    public int GetCurrentPlayerStat(int index) {
        return currentPlayerStats[index];
    }
    public int[] GetCurrentPlayerStats() {
        return currentPlayerStats;
    }

    public void SetCurrentPlayerStats(int index, int amount) {
        this.currentPlayerStats[index] += amount;
    }

    public Dictionary<Statuseffekte, int> GetPlayerEffects() {
        return playerEffects;
    }

    public void AddPlayerEffects(Statuseffekte effect, int duration) {
        if (playerEffects.ContainsKey(effect))
        {
            playerEffects[effect] = duration;
        }
        else
        {
            playerEffects.Add(effect, duration);
        }
    }

    public Dictionary<Items, int> GetCurrentPlayerItems()
    {
        return playerItems;
    }
    public void AddItem(Items item, int amount)
    {
        if (playerItems.ContainsKey(item))
        {
            playerItems[item] += amount;
        }
        else
        {
            playerItems.Add(item, amount);
        }
    }
    public void SetPlayerItems(Dictionary<Items, int> items)
    {
        playerItems = items;
    }

    public void RemoveItem(Items item, int amount)
    {
        if (playerItems.ContainsKey(item) == false)
        {
            Debug.Log("Item nicht vorhanden|| Fehler aus Datenbank");
        }
        if (amount == 0)
        {
            playerItems.Remove(item);
        }
        else if (amount > 0)
        {
            playerItems[item] -= amount;
            if (playerItems[item] <= 0)
            {
                playerItems.Remove(item);
            }
        }
    }

    // opponent

    public Gegner GetCurrentOponent() {
        return currentOponent;
    }

    public void SetCurrentOponent(Gegner currentOponent) {
        this.currentOponent = currentOponent;
    }

    public Attacks[] GetCurrentOponnentAttacks() {
        return currentOponnentAttacks;
    }

    public void SetCurrentOponnentAttacks(Attacks[] currentOponnentAttacks) {
        this.currentOponnentAttacks = currentOponnentAttacks;
    }

    public Dictionary<Statuseffekte, int> GetOpponentEffects() {
        return oponentEffects;
    }

    public void AddOpponentEffects(Statuseffekte effect, int duration) {
        if (oponentEffects.ContainsKey(effect))
        {
            oponentEffects[effect] = duration;
        }
        else
        {
            oponentEffects.Add(effect, duration);
        }
    }

    public int[] GetCurrentOponnentStats() {
        return currentOponnentStats;
    }

    public void SetCurrentOponnentStats(int[] currentOponnentStats) {
        this.currentOponnentStats = currentOponnentStats;
    }


}