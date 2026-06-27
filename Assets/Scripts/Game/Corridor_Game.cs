using UnityEngine;
//made by Dominik
//Updates the camera location when the player enters or leaves a place.
public class Corridor_Game : MonoBehaviour
{
    [SerializeField] private string leadsTo;
    [SerializeField] private string leadsFrom;
    [SerializeField] private bool vertical; // decides whether this is a vertical or horizontal corridor.
    private Camera_Game camera;

    private void Start()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (vertical) //in which direction leads the corridor/hallway
            {
                if (transform.position.y > other.transform.position.y) //player enters from below
                {
                    camera.UpdateCamera(leadsTo); 
                }else                                                  //player enters from above
                {
                    camera.UpdateCamera(leadsFrom);
                }
            }
            else
            {
                if (transform.position.x > other.transform.position.x) //player enters from the left
                {
                    camera.UpdateCamera(leadsTo);
                }else                                                 //player enters from the right
                {
                    camera.UpdateCamera(leadsFrom);
                }
            }
            
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (vertical) //in which direction leads the corridor/hallway
            {
                if (transform.position.y < other.transform.position.y) //player leaves upwards
                {
                    camera.UpdateCamera(leadsTo); 
                }else                                                  //player leaves downwards
                {
                    camera.UpdateCamera(leadsFrom);
                }
            }
            else
            {
                if (transform.position.x < other.transform.position.x) //player leaves to right
                {
                    camera.UpdateCamera(leadsTo);
                }else                                                 //player leaves to left
                {
                    camera.UpdateCamera(leadsFrom);
                }
            }
            
            
        }
    }
}
