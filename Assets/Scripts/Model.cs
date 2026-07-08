using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
public class Model : MonoBehaviour
{
    private Datenbank DB => Datenbank.Instance;

    // player
    public List<Attacks> GetCurrentPlayerAttacks() {
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

    public Dictionary<Statuseffekte, int> GetPlayerEffects() {
        return DB.GetPlayerEffects();
    }
    public Dictionary<Items, int> GetCurrentPlayerItems() {
        return DB.GetCurrentPlayerItems();
    }
    public Dictionary<Items, int> GetSavedPlayerItems() {
        return DB.GetSavedPlayerItems();
    }
    public string GetPlayerLocation()
    {
        return DB.GetPlayerLocation();
    }
    public string GetSavePlayerLocation()
    {
        return DB.GetSavePlayerLocation();
    }

    // opponent

    public Gegner GetCurrentOponent() {
        return DB.GetCurrentOponent();
    }
    public Dictionary<Statuseffekte, int> GetOpponentEffects() {
        return DB.GetOpponentEffects();
    }
    public int[] GetCurrentOponnentStats() {
        return DB.GetCurrentOpponentStats();
    }

    //other things

    public Vector3 GetSpawnPosition()
    {
        return DB.GetSpawnPosition();
    }

    //idk what:
    public void UpdateFightViews()
    {
        FindAnyObjectByType<GM>().DoLoad();
    }

    public void UpdateGAmeViews()
    {

    }

    /*public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

    public void Save()
    {
        DB.Save();
    }



    public void LightsSwitchtoFight(bool fight)
    {
        DB.LightsSwitchToFight(fight);
    }
}