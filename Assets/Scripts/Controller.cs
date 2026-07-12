using UnityEngine;
using System.Collections.Generic;
public class Controller : MonoBehaviour
{
    private Datenbank DB => Datenbank.Instance;


    // player
    public void SetPlayerHealth(int health)
    {
        DB.SetPlayerHealth(health);
    }

    public void SetGegnerHealth(int health)
    {
        DB.SetGegnerHealth(health);
    }

    public void SetPlayerItems(Dictionary<Items, int> items) {
        DB.SetPlayerItems(items);
    }
    public void SetSavePlayerItems(Dictionary<Items, int> items) {
        DB.SetSavePlayerItems(items);
    }

    
    public void AddItem(Items item, int amount) {
        DB.AddItem(item, amount);
    }

    


    // Bei Amount 0 werden alle entfernt
    public void RemoveItem(Items item, int amount)
    {
        DB.RemoveItem(item, amount);
    }
    public void SetPlayerLocation(LocationID playerLocation)
    {
        DB.SetPlayerLocation(playerLocation);
    }
    public void SetSavePlayerLocation(LocationID savePlayerLocation)
    {
        DB.SetSavePlayerLocation(savePlayerLocation);
    }

    // opponent
    public void SetCurrentOponent(Gegner gegner) {
        DB.SetCurrentOponent(gegner);
    }
    

    //other things

    public void SetSpawnPosition(Vector3 spawnPosition)
    {
        DB.SetSpawnPosition(spawnPosition);
    }
    public void NewGame()
    {
        DB.NewGame();
    }

}
