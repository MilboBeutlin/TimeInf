using UnityEngine;

public class Camera_Game : MonoBehaviour
{
    [SerializeField] private Player_Game player;
    [SerializeField] private Transform[] rooms; //empty object for the postion
    [SerializeField] private Transform[] corridors; //empty object for the postion
    private string playerLocation;
    void Start()
    {
        
    }

    void Update()
    {
        playerLocation = player.Location();
        char type = playerLocation[0];
        int index = int.Parse(playerLocation.Substring(1));

        switch (type)
        {
            case 'R':
            transform.position = rooms[index].position;
            break;

            case 'K': //vertical
            transform.position = new Vector2(corridors[index].position.x, player.transform.position.y);
            break;

            case 'G': //horizontal
            transform.position = new Vector2(player.transform.position.x, corridors[index].position.y);
            break;
        }
    }
}
