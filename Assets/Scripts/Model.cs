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

    public void UpdateFightViews()
    {
        FindAnyObjectByType<GM>().Loaded();
    }

    public void UpdateGAmeViews()
    {

    }
}
