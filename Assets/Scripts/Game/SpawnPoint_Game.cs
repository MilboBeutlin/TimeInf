using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint_Game : MonoBehaviour
{

    public static SpawnPoint_Game Instance;
    public Vector3 spawnPosition;
    public Dictionary<Items, int> savedItems = new Dictionary<Items, int>();
    public string playerLocation;
    //[SerializeField] private Player_Game player;
    void Start()
    {
        //player = FindAnyObjectByType<Player_Game>();
    }

    public void moveSpawnPoint(Vector3 newPosition, string playerLocation) {
        spawnPosition = newPosition;
        transform.position = spawnPosition;
        this.playerLocation = playerLocation;
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
}

