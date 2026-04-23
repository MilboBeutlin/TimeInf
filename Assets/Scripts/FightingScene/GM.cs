using UnityEngine;

public class GM : MonoBehaviour
{
    private ButtonManager bM;
    private bool playerturn;

    private Datenbank DB;

    private Attacks[] currentPlayerAttacks;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        DB = FindAnyObjectByType<Datenbank>();
        playerturn = true;

        currentPlayerAttacks = DB.GetCurrentAttacks();
    }
    // Update is called once per frame
    void Update()
    {
        /*
        if(playerturn == false)
        {
            bM.MainButtons();
            bM.blockPlayerButtons();
            
        }

        if(playerturn == true)
        {
            bM.MainButtons();
            bM.openPlayerButtons();
            
        }
        */
    }

    
}
