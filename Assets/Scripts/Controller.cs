using UnityEngine;

public class Controller : MonoBehaviour
{
    private Model model;

    void Start()
    {
        model = FindAnyObjectByType<Model>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // player
    public void SetCurrentPlayerAttacks(Attacks[] attacks) {
        model.SetCurrentPlayerAttacks(attacks);
    }

    public void SetCurrentPlayerStats(int[] stats) {
        model.SetCurrentPlayerStats(stats);
    }

    public void SetCurrentPlayerEffects(Statuseffekte[] effects) {
        model.SetCurrentPlayerEffects(effects);
    }

    public void SetCurrentPlayerItems(Items[] items) {
        model.SetCurrentPlayerItems(items);
    }

    // opponent
    public void SetCurrentOponent(Gegner gegner) {
        model.SetCurrentOponent(gegner);
    }
    public void SetCurrentOponnentAttacks(Attacks[] attacks) {
        model.SetCurrentOponnentAttacks(attacks);
    }
    public void SetCurrentOponentEffects(Statuseffekte[] effects) {
        model.SetCurrentOponentEffects(effects);
    }
    public void SetCurrentOponnentStats(int[] stats) {
        model.SetCurrentOponnentStats(stats);
    }
}
