using UnityEngine;

public class Corridor_Game : MonoBehaviour
{
    [SerializeField] private string leadsTo;
    [SerializeField] private string leadsFrom;
    [SerializeField] private bool vertical;
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
                if (transform.position.y > other.transform.position.y) //player leaves to down
                {
                    camera.UpdateCamera(leadsTo); 
                }else                                                  //player leaves to up
                {
                    camera.UpdateCamera(leadsFrom);
                }
            }
            else
            {
                if (transform.position.x > other.transform.position.x) //player leaves to left
                {
                    camera.UpdateCamera(leadsTo);
                }else                                                 //player leaves to right
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
                if (transform.position.y < other.transform.position.y) //player leaves to down
                {
                    camera.UpdateCamera(leadsTo); 
                }else                                                  //player leaves to up
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
