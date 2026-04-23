using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButtons;


    [SerializeField] private GameObject attackButtons;

    [SerializeField] private GameObject itemButtons;
    [SerializeField] private Transform canvas;

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

    public void Items()
    {
        Instantiate(itemButtons, transform.position, Quaternion.identity, canvas);
        Destroy(mainButtons);
    }



}
