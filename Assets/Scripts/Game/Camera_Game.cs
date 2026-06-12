using UnityEngine;

public class Camera_Game : MonoBehaviour
{
    [SerializeField] private Player_Game player;
    [SerializeField] private Transform[] rooms; //empty object for the postion
    [SerializeField] private Transform[] corridors; //empty object for the postion
    [SerializeField] private Transform[] hallway; //empty object for the postion
    [SerializeField] private Controller controller;
    void Start()
    {
        
    }

    public void UpdateCamera(string leadsTo) //i could use enums but to much work for now
    {
        controller.SetPlayerLocation(leadsTo);
        char type = leadsTo[0];
        int index = int.Parse(leadsTo.Substring(1));

        switch (type)
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
