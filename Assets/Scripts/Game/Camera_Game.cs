using UnityEngine;

public class Camera_Game : MonoBehaviour
{
    [SerializeField] private Player_Game player;
    [SerializeField] private Transform[] rooms; //empty object for the postion
    [SerializeField] private Transform[] corridors; //empty object for the postion
    [SerializeField] private Transform[] hallway; //empty object for the postion
    [SerializeField] private Controller controller;
    [SerializeField] private Model model;
    private string newLocation;
    void Start()
    {
    }
    public void UpdateCamera(string location)
    {
        if(location == null)
        {
            newLocation = "K1";
        }
        else
        {
            newLocation = location;
        }
        controller.SetPlayerLocation(newLocation);
    }

    void Update() //i could use enums but to much work for now
    {
        int index = int.Parse(newLocation.Substring(1));

        switch (newLocation[0]) //what kind of location is it?: room, vertical hallway or horizontal hallway
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
