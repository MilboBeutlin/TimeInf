using UnityEngine;
using System.Collections.Generic;
public class Controller : MonoBehaviour
{
    //[SerializeField] private Datenbank DB;
    private Datenbank DB => Datenbank.Instance;

    void Start()
    {
       // DB = FindAnyObjectByType<Datenbank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/

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

    public void AddPlayerEffects(Statuseffekte effect, int duration) {
        DB.AddPlayerEffects(effect, duration);
    }

    public void SetPlayerItems(Dictionary<Items, int> items) {
        DB.SetPlayerItems(items);
    }
    public void SetSavePlayerItems(Dictionary<Items, int> items) {
        DB.SetSavePlayerItems(items);
    }
    public void AddItem(Items item, int amount) {
        DB.AddItem(item, amount);
    }

    // Bei Amount 0 werden alle entfernt
    public void RemoveItem(Items item, int amount)
    {
        DB.RemoveItem(item, amount);
    }
    public void SetPlayerLocation(string playerLocation)
    {
        DB.SetPlayerLocation(playerLocation);
    }
    public void SetSavePlayerLocation(string savePlayerLocation)
    {
        DB.SetSavePlayerLocation(savePlayerLocation);
    }

    // opponent
    public void SetCurrentOponent(Gegner gegner) {
        DB.SetCurrentOponent(gegner);
    }
    public void SetCurrentOponnentAttacks(Attacks[] attacks) {
        DB.SetCurrentOponnentAttacks(attacks);
    }
    public void AddOpponentEffects(Statuseffekte effect, int duration) {
        DB.AddOpponentEffects(effect, duration);
    }
    public void SetCurrentOponnentStats(int[] stats) {
        DB.SetCurrentOponnentStats(stats);
    }

    //other things

    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        DB.SetSpawnPosition(spawnPosition);
    }
    
}
