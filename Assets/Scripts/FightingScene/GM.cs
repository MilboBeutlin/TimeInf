using UnityEngine;

public class GM : MonoBehaviour
{
    private Model model;
    private ButtonManager bM;

    //Fight logic
    [SerializeField] private bool playerturn;

    //Stats
    [SerializeField] private Attacks[] currentPlayerAttacks;
    [SerializeField] private int currentplayerHealth;
    [SerializeField] private Items[] currentPlayerItems;
    [SerializeField] private Statuseffekte[] currentPlayerStats;

    [SerializeField] private int currentopponentHealth;
    private Attacks[] currentOponnentAttacks;

    private int timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bM = FindAnyObjectByType<ButtonManager>();
        model = FindAnyObjectByType<Model>();

        //Fight Logic
        playerturn = true;

        //Stats
        currentPlayerAttacks = new Attacks[6];
        DoLoad();

    }
    // Update is called once per frame
    void Update()
    {
        //timer einbauen, wenn Gegner gemacht wurde.
    }

    public void DoLoad()
    {
        currentPlayerAttacks = model.GetCurrentPlayerAttacks();
        currentplayerHealth = model.GetCurrentPlayerHealth();
        currentPlayerItems = model.GetCurrentPlayeritems();
        currentPlayerStats = model.GetcurrentPlayerstats();

    }


    public void DoAttack(int y)
    {
        playerturn = false;
        bM.TurnChange();

        Attacks i = currentPlayerAttacks[y];
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

        OponentTurn();
        playerturn = true;

    }


    public Attacks givePlayerAttack(int attack)
    {
        return currentPlayerAttacks[attack];
    }

    public bool GetPlayerturn()
    {
        return playerturn;
    }


    public void OponentTurn()
    {
        
    }
}
