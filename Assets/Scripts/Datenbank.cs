using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

public class Datenbank : MonoBehaviour
{
    public static Datenbank Instance;
    //Alle Stats im Game
    //Player:
    private List<Attacks> playerAttacks = new List<Attacks>();
    private Dictionary<Statuseffekte, int> playerEffects = new Dictionary<Statuseffekte, int>();

    [SerializeField] private Dictionary<Items, int> playerItems = new Dictionary<Items, int>();
    private Dictionary<Items, int> savedPlayerItems = new Dictionary<Items, int>();
    private int[] currentPlayerStats = new int[]{100,25,20,6,0}; //health, attack, armor, speed, dk
    private string playerLocation = "K1";
    private string savePlayerLocation;

    //Gegner:
    private Gegner currentOponent;
    private Attacks[] currentOponnentAttacks;
    private Dictionary<Statuseffekte, int> oponentEffects = new Dictionary<Statuseffekte, int>();
    private int[] currentOponnentStats; //health, attack, armor, speed, dk

    //other stuff:
    private Vector3 spawnPosition;

    private string savePath;

    private void Start()
    {
        //Löscht den Speicherstand
        /*savePath = Path.Combine(Application.persistentDataPath, "savegame.json");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);                         
        }

        Load();*/ 
        
        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
        //Alle stats im Game
        playerAttacks = new List<Attacks>();

        currentOponnentAttacks = new Attacks[6];
        for (int i = 0; i <currentOponnentAttacks.Length; i++)
        {
            currentOponnentAttacks[i] = Attacks.NULL;   
        }

        Load();
        if(playerAttacks.Count() <= 0)
        {
            //starter stuff geaddet
            currentPlayerStats[0] = 100;
            AddItem(Items.Coins, 6);
            AddPlayerAttack(Attacks.Hammer);
            AddPlayerAttack(Attacks.Strike);
            AddItem(Items.Brick, 1);
            AddItem(Items.HealingPotion, 1);
        }
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    //Für Das Speichern
    public void Save()
    {
        GameData data = new GameData();

        data.spawnPointlocation = new float[3];
        data.spawnPointlocation[0] = spawnPosition.x;
        data.spawnPointlocation[1] = spawnPosition.y;
        data.spawnPointlocation[2] = spawnPosition.z;

        data.Attacks = new string[playerAttacks.Count];
        for(int i = 0; i<playerAttacks.Count; i++)
        {
            data.Attacks[i] = playerAttacks[i].ToString();
        }

        data.Items = new string[playerItems.Count];
        for(int i = 0; i <playerItems.Count; i++)
        {
            data.Items[i] = playerItems.ElementAt(i).ToString();
        }

        data.ItemLenght = playerItems.Values.ToArray<int>();
        data.Location = savePlayerLocation;
        data.Stats = currentPlayerStats;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        
    }

    public void Load()
    {
        if (File.Exists(savePath))
        { 
            string json = File.ReadAllText(savePath);

            GameData data = JsonUtility.FromJson<GameData>(json);

            spawnPosition.x = data.spawnPointlocation[0];
            spawnPosition.y = data.spawnPointlocation[1];
            spawnPosition.z = data.spawnPointlocation[2];

            for (int i = 0; i < data.Attacks.Length; i++)
            {
                if (Enum.TryParse<Attacks>(data.Attacks[i], out Attacks result))
                {
                    playerAttacks.Add(Enum.Parse<Attacks>(data.Attacks[i]));
                }
            }

            for (int i = 0; i < data.Items.Length; i++)
            {

                if (Enum.TryParse<Items>(data.Items[i], out  Items result)) {
                    playerItems.Add(Enum.Parse<Items>(data.Items[i]), data.ItemLenght[i]);

                }
            }

            currentPlayerStats = data.Stats;
            savePlayerLocation = data.Location;
            

        } else
        {
            //starter stuff geaddet
            currentPlayerStats[0] = 100;
            AddItem(Items.Coins, 6);
            AddPlayerAttack(Attacks.Hammer);
            AddPlayerAttack(Attacks.Strike);
            AddItem(Items.Brick, 1);
            AddItem(Items.HealingPotion, 1);
        }
        
    }

    

    //Alle Stats im Game
    //player
    public List<Attacks> GetCurrentPlayerAttacks() {
        return playerAttacks;
    }

    public void SetCurrentPlayerAttacks(List<Attacks> playerAttacks) {
        this.playerAttacks = playerAttacks;
    }

    public int GetCurrentPlayerStat(int index) {
        return currentPlayerStats[index];
    }
    //Falls man den gesammten Array gleichzeitig setzten möchte:
    public void SetCurrentPlayerFULLStats(int[] i )
    {
        currentPlayerStats = i;
    }
    public int[] GetCurrentPlayerStats() {
        return currentPlayerStats;
    }

    public void SetPlayerEffects(Dictionary<Statuseffekte, int> i)
    {
        playerEffects = i;
    }

    public void SetCurrentPlayerStats(int index, int amount) {
        this.currentPlayerStats[index] += amount;
    }

    public Dictionary<Statuseffekte, int> GetPlayerEffects() {
        return playerEffects;
    }

    public void AddPlayerEffects(Statuseffekte effect, int duration) {
        if (playerEffects.ContainsKey(effect))
        {
            playerEffects[effect] = duration;
        }
        else
        {
            playerEffects.Add(effect, duration);
        }
    }

    public void AddPlayerAttack(Attacks attack)
    {
        playerAttacks.Add(attack);
    }

    public Dictionary<Items, int> GetCurrentPlayerItems()
    {
        return playerItems;
    }
    public void AddItem(Items item, int amount)
    {
        if (playerItems.ContainsKey(item))
        {
            playerItems[item] += amount;
        }
        else
        {
            playerItems.Add(item, amount);
        }
    }
    public void SetPlayerItems(Dictionary<Items, int> items)
    {
        playerItems = items;
    }
    public void SetSavePlayerItems(Dictionary<Items, int> items)
    {
        savedPlayerItems = items;
    }
    public Dictionary<Items, int> GetSavedPlayerItems()
    {
        return savedPlayerItems;
    }

    public void RemoveItem(Items item, int amount)
    {
        if (playerItems.ContainsKey(item) == false)
        {
            Debug.Log("Item nicht vorhanden|| Fehler aus Datenbank");
        }
        if (amount == 0)
        {
            playerItems.Remove(item);
        }
        else if (amount > 0)
        {
            playerItems[item] -= amount;
            if (playerItems[item] <= 0)
            {
                playerItems.Remove(item);
            }
        }
    }
    public string GetPlayerLocation()
    {
        return playerLocation;
    }
    public void SetPlayerLocation(string playerLocation)
    {
        this.playerLocation = playerLocation;
    }
    public string GetSavePlayerLocation()
    {
        return savePlayerLocation;
    }
    public void SetSavePlayerLocation(string savePlayerLocation)
    {
        this.savePlayerLocation = savePlayerLocation;
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

    public Dictionary<Statuseffekte, int> GetOpponentEffects() {
        return oponentEffects;
    }

    public void AddOpponentEffects(Statuseffekte effect, int duration) {
        if (oponentEffects.ContainsKey(effect))
        {
            oponentEffects[effect] = duration;
        }
        else
        {
            oponentEffects.Add(effect, duration);
        }
    }

    public int[] GetCurrentOpponentStats()
    {
        Debug.Log(currentOponent);
        return currentOponent switch
        {
            Gegner.StorageGuard => new int[] {15, 30, 100, 3, 5},
            Gegner.MonsterPainting => new int[] { 30, 35, 40, 9, 30 },
            Gegner.ShadowEnemy => new int[] { 10, 50, 60, 7, 90 },
            Gegner.Insects => new int[] { 25, 25, 0, 4, 55 },
            Gegner.PrisonGuard => new int[] { 100, 55, 30, 5, 35 },
            Gegner.MiniBoss => new int[] { 160, 75, 10, 8, 100 },
            Gegner.Endboss => new int[] { 300, 90, 30, 9, 120 },
            _ => new int[] { 100, 10, 0, 0 }
        };
    }

    public void SetCurrentOponnentStats(int[] currentOponnentStats) {
        this.currentOponnentStats = currentOponnentStats;
    }

    //other things:
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }

}