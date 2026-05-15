using UnityEngine;

public class Camera_Game : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] rooms; //empty object for the postion
    [SerializeField] private Transform[] corridors; //empty object for the postion
    private string PlayerPosition; //<-- where are you? which corridor/room; String or int? or enum?
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = player.position; // für den moment nur da

        /*switch (PlayerPosition)
        {
            case "R1":
            this.transform.position = rooms[0].position;
            break;
            case "K1":
            this.transform.position = player.position.y; 
            this.transform.position = corridors[0].position.x;
            break;
            ...
        }*/
    }
}
