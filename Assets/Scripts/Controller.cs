using UnityEngine;

public class Controller : MonoBehaviour
{
    private Datenbank DB;

    void Start()
    {
        DB = FindAnyObjectByType<Datenbank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // player
    public void SetCurrentPlayerAttacks(Attacks[] attacks) {
        DB.SetCurrentPlayerAttacks(attacks);
    }

    public void IncreasePlayerLp(int amount) {
        DB.SetCurrentPlayerStats(0, amount);
    }
    public void IncreasePlayerAtk(int amount) {
        DB.SetCurrentPlayerStats(1, amount);
    }
    public void IncreasePlayerArmor(int amount) {
        DB.SetCurrentPlayerStats(2, amount);
    }
    public void IncreasePlayerSpeed(int amount) {
        DB.SetCurrentPlayerStats(3, amount);
    }
    public void IncreasePlayerDk(int amount) {
        DB.SetCurrentPlayerStats(4, amount);
    }

    public void SetCurrentPlayerEffects(Statuseffekte[] effects) {
        DB.SetCurrentPlayerEffects(effects);
    }

    public void AddItem(Items item, int amount) {
        DB.AddItem(item, amount);
    }

    // opponent
    public void SetCurrentOponent(Gegner gegner) {
        DB.SetCurrentOponent(gegner);
    }
    public void SetCurrentOponnentAttacks(Attacks[] attacks) {
        DB.SetCurrentOponnentAttacks(attacks);
    }
    public void SetCurrentOponentEffects(Statuseffekte[] effects) {
        DB.SetCurrentOponentEffects(effects);
    }
    public void SetCurrentOponnentStats(int[] stats) {
        DB.SetCurrentOponnentStats(stats);
    }
}
