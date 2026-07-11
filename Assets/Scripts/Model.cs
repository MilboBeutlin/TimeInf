using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
public class Model : MonoBehaviour
{
    private Datenbank DB => Datenbank.Instance;

    // player
    public int GetPlayerHealth()
    {
        return DB.GetPlayerHEalth();
    }

    public int GetGegnerHealth()
    {
        return DB.GetOponentHEalth();
    }



    public Dictionary<Items, int> GetCurrentPlayerItems() {
        return DB.GetCurrentPlayerItems();
    }
    public Dictionary<Items, int> GetSavedPlayerItems() {
        return DB.GetSavedPlayerItems();
    }
    public LocationID GetPlayerLocation()
    {
        return DB.GetPlayerLocation();
    }
    public LocationID GetSavePlayerLocation()
    {
        return DB.GetSavePlayerLocation();
    }

    // opponent

    public Gegner GetCurrentOponent() {
        return DB.GetCurrentOponent();
    }
    
    public int GetCurrentOponnentStats() {
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



    public void LightsSwitchToFight(bool fight)
    {
        DB.LightsSwitchToFight(fight);
    }
}