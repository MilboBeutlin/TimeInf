using UnityEngine;

public class GM : MonoBehaviour
{
    private ButtonManager bM;
    private bool playerturn;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        playerturn = true;
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
