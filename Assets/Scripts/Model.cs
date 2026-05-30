using UnityEngine;
using System.Collections.Generic;
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
    public int GetCurrentPlayerLp() {
        return DB.GetCurrentPlayerStat(0);
    }
    public int GetCurrentPlayerAtk() {
        return DB.GetCurrentPlayerStat(1);
    }
    public int GetCurrentPlayerArmor() {
        return DB.GetCurrentPlayerStat(2);
    }
    public int GetCurrentPlayerSpeed() {
        return DB.GetCurrentPlayerStat(3);
    }
    public int GetCurrentPlayerDk() {
        return DB.GetCurrentPlayerStat(4);
    }

    public Statuseffekte[] GetCurrentPlayerEffects() {
        return DB.GetCurrentPlayerEffects();
    }
    public Dictionary<Items, int> GetCurrentPlayerItems() {
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