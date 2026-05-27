using UnityEngine;

public class Corridor_Game : MonoBehaviour
{
    [SerializeField] private string leadsTo;
    [SerializeField] private string leadsFrom;
    [SerializeField] private bool vertical;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Game player = other.GetComponent<Player_Game>();
            if (vertical) //in which direction leads the corridor/hallway
            {
                if (transform.position.y > other.transform.position.y) //player leaves to down
                {
                    player.SetLocation(leadsTo); 
                }else                                                  //player leaves to up
                {
                    player.SetLocation(leadsFrom);
                }
            }
            else
            {
                if (transform.position.x > other.transform.position.x) //player leaves to left
                {
                    player.SetLocation(leadsTo);
                }else                                                 //player leaves to right
                {
                    player.SetLocation(leadsFrom);
                }
            }
            
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Game player = other.GetComponent<Player_Game>();
            if (vertical) //in which direction leads the corridor/hallway
            {
                if (transform.position.y < other.transform.position.y) //player leaves to down
                {
                    player.SetLocation(leadsTo); 
                }else                                                  //player leaves to up
                {
                    player.SetLocation(leadsFrom);
                }
            }
            else
            {
                if (transform.position.x < other.transform.position.x) //player leaves to right
                {
                    player.SetLocation(leadsTo);
                }else                                                 //player leaves to left
                {
                    player.SetLocation(leadsFrom);
                }
            }
            
            
        }
    }
}
