using UnityEngine;

public class Door_Game : MonoBehaviour
{

    [SerializeField] private Transform targetDoorSP;
    [SerializeField] private string leadsTo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(targetDoorSP != null)
            {
                other.transform.position = targetDoorSP.position; //moves Player to other door
            }

            Player_Game player = other.GetComponent<Player_Game>(); //set new Location
            player.SetLocation(leadsTo);
        }
    }
}
