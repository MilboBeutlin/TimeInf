using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject mainButtons;


    public GameObject attackButtons;
    public Transform canvas;

    private void Start()
    {
        Instantiate(mainButtons, transform.position, Quaternion.identity, canvas);
    }
    /*
    //hierfür wird ein anderes Script beim Main Button Objekt gebraucht
    public void blockPlayerButtons()
    {
        Attackbutton.interactable = false;
        Itembutton.interactable = false;
        Runbutton.interactable = false;
    }

    public void openPlayerButtons()
    {
        Attackbutton.interactable = true;
        Itembutton.interactable = true;
        Runbutton.interactable = true;
    }
    */

    public void Attacks()
    {
        Instantiate(attackButtons, transform.position, Quaternion.identity, canvas );
        Destroy(mainButtons);
        
    }
    public void MainButtons()
    {
        Instantiate(mainButtons, transform.position, Quaternion.identity, canvas);
        Destroy(attackButtons);
    }

    


}
