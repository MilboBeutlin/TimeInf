using UnityEngine;

public class Datenbank : MonoBehaviour
{
    private Model model;
    
    //Alle Stats im Game
    private Attacks[] currentPlayerAttacks;
    private int currentPlayerHealth;
    private Attacks[] currentOponnentAttacks;
    private int currentOponnentHealth;


    private void Start()
    {
        model = FindAnyObjectByType<Model>();

        //Alle stats im Game
        currentPlayerAttacks = new Attacks[6];
        for (int i = 0; i < currentPlayerAttacks.Length; i++)
        {
            currentPlayerAttacks[i] = Attacks.NULL;
        }

    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    //Alle Satas im Game
    public Attacks[] GetCurrentAttacks()
    {
        return currentPlayerAttacks;
    }

    

}
