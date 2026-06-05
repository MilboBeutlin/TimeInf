using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint_Game : MonoBehaviour
{

    public static SpawnPoint_Game Instance;
    private Vector3 spawnPosition;
    private Dictionary<Items, int> savedItems = new Dictionary<Items, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

