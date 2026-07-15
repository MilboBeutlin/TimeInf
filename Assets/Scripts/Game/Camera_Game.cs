using UnityEngine;

// Positions the camera based on the player's current location.
public class Camera_Game : MonoBehaviour
{
    [SerializeField] private Player_Game player;
    [SerializeField] private Transform[] rooms; //empty object for the postion
    [SerializeField] private Transform[] corridors; //empty object for the postion
    [SerializeField] private Transform[] hallway; //empty object for the postion
    [SerializeField] private Controller controller;
    private LocationID newLocation;
    private char type;
    private int index;

    public void UpdateCamera(LocationID location)
    {
        if (location == LocationID.None) //Default spawn location
        {
            newLocation = LocationID.K1;
        }
        else
        {
            newLocation = location;
        }
        controller.SetPlayerLocation(newLocation);

        //translates the locationID into a type and index for the camera to use
        string id = newLocation.ToString();
        type = id[0];
        index = int.Parse(id.Substring(1));
    }

    void Update()
    {
        switch (type) // First character of location is the location type.
        {
            case 'R': //rooms
                transform.position = rooms[index].position;
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
