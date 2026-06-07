using UnityEngine;
using System.Collections.Generic;

public class SpawnPoint_Game : MonoBehaviour
{

    public static SpawnPoint_Game Instance;
    public Vector3 spawnPosition;
    public Dictionary<Items, int> savedItems = new Dictionary<Items, int>();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}

