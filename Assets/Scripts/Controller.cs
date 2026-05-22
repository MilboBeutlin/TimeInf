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

    public void SetCurrentPlayerStats(int[] stats) {
        DB.SetCurrentPlayerStats(stats);
    }

    public void SetCurrentPlayerEffects(Statuseffekte[] effects) {
        DB.SetCurrentPlayerEffects(effects);
    }

    public void SetCurrentPlayerItems(Items[] items) {
        DB.SetCurrentPlayerItems(items);
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
