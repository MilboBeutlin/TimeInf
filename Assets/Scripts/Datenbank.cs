using UnityEngine;

public class Datenbank : MonoBehaviour
{
    private Attacks[] currentPlayerAttacks;

    private void Start()
    {
        currentPlayerAttacks = new Attacks[6];
        for (int i = 0; i < currentPlayerAttacks.Length; i++)
        {
            currentPlayerAttacks[i] = Attacks.NULL;
        }
    }

    public Attacks[] GetCurrentAttacks()
    {
        return currentPlayerAttacks;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
