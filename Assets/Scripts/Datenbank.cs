using UnityEngine;

public class Datenbank : MonoBehaviour
{
    private Model model;

    //Alle Stats im Game
    private Attacks[] currentPlayerAttacks;
    private int currentPlayerHealth;
    private Statuseffekte[] currentPlayerStat;
    private Items[] currentPlayerItems;

    private Gegner currentOponent;
    private Attacks[] currentOponnentAttacks;
    private int currentOponnentHealth;
    private Statuseffekte[] currentOponentStats;
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
    public Attacks[] GetCurrentAttacks()
    {
        return currentPlayerAttacks;
    }

    public int GetCurrentPlayerHealth()
    {
        return currentPlayerHealth;
    }

    public Statuseffekte[] GetCurrentPlayetStats()
    {
        return currentPlayerStat;
    }

    public Items[] GetCurrentPlayeritems()
    {
        return currentPlayerItems;
    }

    public Attacks[] GetCurrentOponnentAttacks()
    {
        return currentOponnentAttacks;
    }

    public int GetCurrentOponnentStats()
    {
        return currentOponnentStats[5];
    }

    public void SetCurrentOpponentAttacks(Attacks[] attacks)
    {
        currentOponnentAttacks = attacks;
    }

    public void SetCurrentOpponentStats(int[] stats)
    {
        currentOponnentStats = stats;
    }


}