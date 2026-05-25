using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButtons;

    [SerializeField] private GameObject attackButtons;

    private GM gm;

    [SerializeField] private GameObject itemButtons;
    [SerializeField] private Transform canvas;

    [SerializeField] private GameObject[] ItembuttonsList;



    private void Start()
    {
        MainButton();
        gm = FindAnyObjectByType<GM>();

        CheckItems();
    }

    public void MainButton()
    {
        mainButtons.SetActive(true);
        attackButtons.SetActive(false);
        itemButtons.SetActive(false);
    }
    public void AttackButtons()
    {
        mainButtons.SetActive(false);
        attackButtons.SetActive(true);
        itemButtons.SetActive(false);
    }
    public void ItemButtons()
    {
        mainButtons.SetActive(false);
        attackButtons.SetActive(false);
        itemButtons.SetActive(true);
        
        
    }

    private void Update()
    {

    }



    public void TurnChange(bool i)
    {
        MainButton();
        mainButtons.GetComponent<MainButtonScript>().SetMainButtonActive(i);
    }

    public void CheckItems()
    {
        // Alle Knöpfe deaktivieren, die NULL sind.
        for (int j = 0; j < ItembuttonsList.Length; j++)
        {
            
            for (int i = 0; i < gm.giveCurrentPlayerItems().Length; i++)
            {
                if (ItembuttonsList[j].GetComponent<MainButtonScript>().giveItemButtonType() == gm.giveCurrentPlayerItems()[i] && gm.giveCurrentPlayerItems()[i] != Items.NULL)
                {
                    ItembuttonsList[j].SetActive(true);
                    j++;
                    i = 0;
                } else 
                {
                    ItembuttonsList[j].SetActive(false);
                }
                
                
            }
            
        }
    }

    
}
