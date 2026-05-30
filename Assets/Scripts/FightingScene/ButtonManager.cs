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
        // Alle Kn�pfe deaktivieren, die NULL sind.
        for (int i = 0; i < ItembuttonsList.Length; i++)
        {
            Items buttonItem = ItembuttonsList[i].GetComponent<MainButtonScript>().giveItemButtonType();
            ItembuttonsList[i].SetActive(gm.giveCurrentPlayerItems().ContainsKey(buttonItem));
        }
    }    
}
