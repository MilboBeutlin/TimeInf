using UnityEngine;
//made by Dominik
//Updates the camera location when the player leaves a corridor.
public class Corridor_Game : MonoBehaviour
{
    [SerializeField] private string leadsTo; //to where it leads: the place on the right to the collider
    [SerializeField] private string leadsFrom; //from where it leads: the place on the left to the collider
    [SerializeField] private bool vertical; // decides whether this is a vertical or horizontal corridor.
    private Camera_Game camera;

    private void Start()
    {
        camera = FindAnyObjectByType<Camera_Game>();
    }
    private void OnTriggerExit2D(Collider2D other) // checks if the player goes into the new location or turns around 
    {
        Rigidbody2D rb = other.attachedRigidbody;

        if (vertical)
        {
            if (rb.linearVelocity.y > 0) //checks if the player moves up or down and Update Camera accordingly
            {
                camera.UpdateCamera(leadsTo);
            }
            else
            {
                camera.UpdateCamera(leadsFrom);
            }
        }
        else
        {
            if (rb.linearVelocity.x > 0) //checks if the player moves left or right and Update Camera accordingly
            {
                camera.UpdateCamera(leadsTo);
            }
            else
            {
                camera.UpdateCamera(leadsFrom);
            }
        }
    }
}
