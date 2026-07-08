using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Datenbank : MonoBehaviour
{
    public static Datenbank Instance;
    //Alle Stats im Game
    //Player:


    private bool playerHasSwim;
    [SerializeField] private Dictionary<Items, int> playerItems = new Dictionary<Items, int>();
    private Dictionary<Items, int> savedPlayerItems = new Dictionary<Items, int>();
    
    private string playerLocation = "K1";
    private string savePlayerLocation;

    //Gegner:
    private Gegner currentOponent;

    //new Fighting Stuff
    private int playerHealth = 5;
    private int gegnerHEalth;
    private List<Attacks> fokusedAttacks;
    private List<Attacks> unfokusedAttacks;

    //other stuff:
    private Vector3 spawnPosition;

    private string savePath;
    [SerializeField] private GameObject gameLight;

    public void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");

        //Alle stats im Game
        
        playerItems = new Dictionary<Items, int>();
        
        gameLight = GameObject.FindGameObjectWithTag("GlobalLight");

        
        
        
        Load();

        
    }
    public void NewGame()
    {
        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        Load(); // Load the default game state after deleting the save file
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

        

        data.Items = new string[playerItems.Count];
        for(int i = 0; i <playerItems.Count; i++)
        {
            data.Items[i] = playerItems.ElementAt(i).ToString();
        }

        data.ItemLenght = playerItems.Values.ToArray<int>();
        data.Location = savePlayerLocation;
        data.Stats = playerHealth;

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

            

            for (int i = 0; i < data.Items.Length; i++)
            {

                if (Enum.TryParse<Items>(data.Items[i], out  Items result)) {
                    playerItems.Add(Enum.Parse<Items>(data.Items[i]), data.ItemLenght[i]);

                }
            }
            playerHealth = data.Stats;
            savePlayerLocation = data.Location;
            

        } else
        {
            //start location player
            spawnPosition = new Vector3(0, 0, 0);
            savePlayerLocation = "K1";

            //resett
            
            playerItems = new Dictionary<Items, int>();
            

            
        }
        
    }

    

    //Alle Stats im Game
    //player
    

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


    //New Fight

    public List<Attacks> GetAttacks(bool fokused)
    {
        if(fokused) { return fokusedAttacks; }
        else { return unfokusedAttacks; }
    }

    public void SetAttacks(List<Attacks> attacks, bool fokused)
    {
        if(fokused)
        {
            fokusedAttacks = attacks;
        } else
        {
            unfokusedAttacks = attacks;
        }
    }

    public int GetPlayerHEalth()
    {
        return playerHealth;
    }
    public void SetPlayerHEalth(int health)
    {
        playerHealth = health;
    }

    public int GetOponentHEalth()
    {
        return gegnerHEalth;
    }

    public void SetGegnerHEalth(int health)
    {
        gegnerHEalth = health;
    }
    // opponent

    public Gegner GetCurrentOponent() {
        return currentOponent;
    }

    public void SetCurrentOponent(Gegner currentOponent) {
        this.currentOponent = currentOponent;
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

    

    //other things:
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        this.spawnPosition = spawnPosition;
    }


    //Lights
    public void LightsSwitchToFight(bool switchToFight)
    {
        gameLight.GetComponent<Light2D>().intensity = switchToFight ? 0.03f : 0.25f;
    }

}