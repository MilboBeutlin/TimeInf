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

    public int GetCurrentOpponentHealth()
    {
        return DB.GetCurrentOponnentHealth();
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
