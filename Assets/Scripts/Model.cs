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

    public Attacks[] GetCurrentPlayerAttacks()
    {
        return DB.GetCurrentAttacks();
    }

    public int GetCurrentPlayerHealth()
    {
        return DB.GetCurrentPlayerHealth();
    }
    public Attacks[] GetCurrentOpponentAttacks()
    {
        return DB.GetCurrentOponnentAttacks();
    }

    public int GetCurrentOpponentStats()
    {
        return DB.GetCurrentOponnentStats();
    }

    public Statuseffekte[] GetcurrentPlayerstats()
    {
        return DB.GetCurrentPlayetStats();
    }
    public Items[] GetCurrentPlayeritems()
    {
        return DB.GetCurrentPlayeritems();
    }


    public void SetCurrentOpponentAttacks(Attacks[] attacks)
    {
        DB.SetCurrentOpponentAttacks(attacks);
    }


    public void SetCurrentOpponentStats(int[] stats)
    {
        DB.SetCurrentOpponentStats(stats);
    }
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