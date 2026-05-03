using UnityEngine;

public class GM : MonoBehaviour
{
    private Model model;
    private ButtonManager bM;

    //Fight logic
    private bool playerturn;

    //Stats
    [SerializeField] private Attacks[] currentPlayerAttacks;
    [SerializeField] private int currentplayerHealth;
    [SerializeField] private int currentopponentHealth;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();

        //Fight Logic
        playerturn = true;

        //Stats
        currentPlayerAttacks = new Attacks[6];
        currentPlayerAttacks = model.GetCurrentPlayerAttacks();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Loaded()
    {
        currentPlayerAttacks = model.GetCurrentPlayerAttacks();
    }

    public void DoAttack(Attacks i)
    {
        switch (i)
        {
            case Attacks.NULL:
                break;

            case Attacks.Donnerschock:
                break;

            default:
                Debug.Log("Gooner");
                break;
        }
    }


    public Attacks givePlayerAttack(int attack)
    {
        return currentPlayerAttacks[attack];
    }

    

    



}
