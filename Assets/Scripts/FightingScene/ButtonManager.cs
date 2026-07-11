using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private GameObject itemButtons;

    private GM gm;
    
    [SerializeField] private Transform canvas;

    [SerializeField] private GameObject[] ItembuttonsList;




    private void Start()
    {
        MainButton();
        gm = FindAnyObjectByType<GM>();

        CheckItems();
    }



    // Methoden zur aktivierung und deaktivierung der Knöpfe.
    public void MainButton()
    {
        if(mainButtons &&  itemButtons)
        {
            mainButtons.SetActive(true);
            itemButtons.SetActive(false);
        }
    }

    public void ItemButtons()
    {
        Debug.Log("Methode aktiviert");
        if(mainButtons &&  itemButtons)
        {
            Debug.Log("debuging");
            mainButtons.SetActive(false);
            itemButtons.SetActive(true);
        }         
    }

    

    //Disabled alle Itemknöpfe, bei denen die Items nicht im Inventar liegen
    public void CheckItems()
    {
        // Alle Kn�pfe deaktivieren, die NULL sind.
        for (int i = 0; i < ItembuttonsList.Length; i++)
        {
            Items buttonItem = ItembuttonsList[i].GetComponent<MainButtonScript>().giveItemButtonType();
            ItembuttonsList[i].SetActive(gm.giveCurrentPlayerItems().ContainsKey(buttonItem));
        }
    }    
}
