using UnityEngine;

public class Datenbank : MonoBehaviour
{
    private Model model;

    //Alle Stats im Game
    private Attacks[] currentPlayerAttacks;
    private int[] currentPlayerStats;
    private Statuseffekte[] currentPlayerEffects;
    private Items[] currentPlayerItems;

    private Gegner currentOponent;
    private Attacks[] currentOponnentAttacks;
    private Statuseffekte[] currentOponentEffects;
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
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    //Alle Satas im Game
    //player
    public Attacks[] GetCurrentPlayerAttacks() {
        return currentPlayerAttacks;
    }

    public void SetCurrentPlayerAttacks(Attacks[] currentPlayerAttacks) {
        this.currentPlayerAttacks = currentPlayerAttacks;
    }

    public int[] GetCurrentPlayerStats() {
        return currentPlayerStats;
    }

    public void SetCurrentPlayerStats(int[] currentPlayerStats) {
        this.currentPlayerStats = currentPlayerStats;
    }

    public Statuseffekte[] GetCurrentPlayerEffects() {
        return currentPlayerEffects;
    }

    public void SetCurrentPlayerEffects(Statuseffekte[] currentPlayerEffects) {
        this.currentPlayerEffects = currentPlayerEffects;
    }

    public Items[] GetCurrentPlayerItems() {
        return currentPlayerItems;
    }

    public void SetCurrentPlayerItems(Items[] currentPlayerItems) {
        this.currentPlayerItems = currentPlayerItems;
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

    public Statuseffekte[] GetCurrentOponentEffects() {
        return currentOponentEffects;
    }

    public void SetCurrentOponentEffects(Statuseffekte[] currentOponentEffects) {
        this.currentOponentEffects = currentOponentEffects;
    }

    public int[] GetCurrentOponnentStats() {
        return currentOponnentStats;
    }

    public void SetCurrentOponnentStats(int[] currentOponnentStats) {
        this.currentOponnentStats = currentOponnentStats;
    }


}