using UnityEngine;

public class Model : MonoBehaviour
{

    private Datenbank DB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DB = FindAnyObjectByType<Datenbank>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    // player

    public Attacks[] GetCurrentPlayerAttacks() {
        return DB.GetCurrentPlayerAttacks();
    }

    public void SetCurrentPlayerAttacks(Attacks[] attacks) {
        DB.SetCurrentPlayerAttacks(attacks);
    }

    public int[] GetCurrentPlayerStats() {
        return DB.GetCurrentPlayerStats();
    }

    public void SetCurrentPlayerStats(int[] stats) {
        DB.SetCurrentPlayerStats(stats);
    }

    public Statuseffekte[] GetCurrentPlayerEffects() {
        return DB.GetCurrentPlayerEffects();
    }

    public void SetCurrentPlayerEffects(Statuseffekte[] effects) {
        DB.SetCurrentPlayerEffects(effects);
    }

    public Items[] GetCurrentPlayerItems() {
        return DB.GetCurrentPlayerItems();
    }

    public void SetCurrentPlayerItems(Items[] items) {
        DB.SetCurrentPlayerItems(items);
    }

    // opponent

    public Gegner GetCurrentOponent() {
        return DB.GetCurrentOponent();
    }

    public void SetCurrentOponent(Gegner gegner) {
        DB.SetCurrentOponent(gegner);
    }

    public Attacks[] GetCurrentOponnentAttacks() {
        return DB.GetCurrentOponnentAttacks();
    }

    public void SetCurrentOponnentAttacks(Attacks[] attacks) {
        DB.SetCurrentOponnentAttacks(attacks);
    }

    public Statuseffekte[] GetCurrentOponentEffects() {
        return DB.GetCurrentOponentEffects();
    }

    public void SetCurrentOponentEffects(Statuseffekte[] effects) {
        DB.SetCurrentOponentEffects(effects);
    }

    public int[] GetCurrentOponnentStats() {
        return DB.GetCurrentOponnentStats();
    }

    public void SetCurrentOponnentStats(int[] stats) {
        DB.SetCurrentOponnentStats(stats);
    }
    

    //idk what:
    public void UpdateFightViews()
    {
        FindAnyObjectByType<GM>().DoLoad();
    }

    public void UpdateGAmeViews()
    {

    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}