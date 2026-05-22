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
    public int[] GetCurrentPlayerStats() {
        return DB.GetCurrentPlayerStats();
    }
    public Statuseffekte[] GetCurrentPlayerEffects() {
        return DB.GetCurrentPlayerEffects();
    }
    public Items[] GetCurrentPlayerItems() {
        return DB.GetCurrentPlayerItems();
    }
    // opponent

    public Gegner GetCurrentOponent() {
        return DB.GetCurrentOponent();
    }
    public Attacks[] GetCurrentOponnentAttacks() {
        return DB.GetCurrentOponnentAttacks();
    }
    public Statuseffekte[] GetCurrentOponentEffects() {
        return DB.GetCurrentOponentEffects();
    }
    public int[] GetCurrentOponnentStats() {
        return DB.GetCurrentOponnentStats();
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