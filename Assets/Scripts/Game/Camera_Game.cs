using UnityEngine;
//made by Dominik
// Positions the camera based on the player's current location.
public class Camera_Game : MonoBehaviour
{
    [SerializeField] private Player_Game player;
    [SerializeField] private Transform[] rooms; //empty object for the postion
    [SerializeField] private Transform[] corridors; //empty object for the postion
    [SerializeField] private Transform[] hallway; //empty object for the postion
    [SerializeField] private Controller controller;
    private string newLocation;
    void Start()
    {
    }
    public void UpdateCamera(string location) 
    {
        if(string.IsNullOrEmpty(location)) //Default spawn location
        {
            newLocation = "K1";
        }
        else
        {
            newLocation = location;
        }
        controller.SetPlayerLocation(newLocation);
    }

    void Update()
    {
        int index = int.Parse(newLocation.Substring(1)); // Gets the location index (e.g. "R3" -> 3)

        switch (newLocation[0]) // First character of location is the location type.
        {
            case 'R': //rooms
            transform.position = rooms[index-1].position;
            break;

            case 'K': //vertical hallway
            transform.position = new Vector2(corridors[index].position.x, player.transform.position.y);
            break;

            case 'G': //horizontal hallway
            transform.position = new Vector2(player.transform.position.x, hallway[index].position.y);
            break;
        }    
    }
}
