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

    public Attacks[] GetCurrentPlayerAttacks() {
        return model.GetCurrentPlayerAttacks();
    }

    public void SetCurrentPlayerAttacks(Attacks[] attacks) {
        model.SetCurrentPlayerAttacks(attacks);
    }

    public int[] GetCurrentPlayerStats() {
        return model.GetCurrentPlayerStats();
    }

    public void SetCurrentPlayerStats(int[] stats) {
        model.SetCurrentPlayerStats(stats);
    }

    public Statuseffekte[] GetCurrentPlayerEffects() {
        return model.GetCurrentPlayerEffects();
    }

    public void SetCurrentPlayerEffects(Statuseffekte[] effects) {
        model.SetCurrentPlayerEffects(effects);
    }

    public Items[] GetCurrentPlayerItems() {
        return model.GetCurrentPlayerItems();
    }

    public void SetCurrentPlayerItems(Items[] items) {
        model.SetCurrentPlayerItems(items);
    }

    // opponent

    public Gegner GetCurrentOponent() {
        return model.GetCurrentOponent();
    }

    public void SetCurrentOponent(Gegner gegner) {
        model.SetCurrentOponent(gegner);
    }

    public Attacks[] GetCurrentOponnentAttacks() {
        return model.GetCurrentOponnentAttacks();
    }

    public void SetCurrentOponnentAttacks(Attacks[] attacks) {
        model.SetCurrentOponnentAttacks(attacks);
    }

    public Statuseffekte[] GetCurrentOponentEffects() {
        return model.GetCurrentOponentEffects();
    }

    public void SetCurrentOponentEffects(Statuseffekte[] effects) {
        model.SetCurrentOponentEffects(effects);
    }

    public int[] GetCurrentOponnentStats() {
        return model.GetCurrentOponnentStats();
    }

    public void SetCurrentOponnentStats(int[] stats) {
        model.SetCurrentOponnentStats(stats);
    }
}
