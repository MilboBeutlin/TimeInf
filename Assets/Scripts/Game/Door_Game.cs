using UnityEngine;

public class Door_Game : MonoBehaviour
{

    [SerializeField] private Transform targetDoorSP;
    [SerializeField] private string leadsTo;
    [SerializeField] private GameObject darkness;
    //[SerializeField] private GameObject torches;
    [SerializeField] private Model model;
    [SerializeField] private Camera_Game camera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(targetDoorSP != null)
            {
                if(leadsTo == "R6" && model.GetCurrentPlayerItems().ContainsKey(Items.Feuerzeug))
                {
                    darkness.SetActive(false);
                    //torches.SetActive(true);
                }
                other.transform.position = targetDoorSP.position; //moves Player to other door
            }
            
            camera.UpdateCamera(leadsTo);
        }
    }
}
