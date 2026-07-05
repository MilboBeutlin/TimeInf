using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButtons;

    [SerializeField] private GameObject attackButtons;

    private GM gm;

    [SerializeField] private GameObject itemButtons;
    [SerializeField] private Transform canvas;

    [SerializeField] private GameObject[] ItembuttonsList;

    [SerializeField] private GameObject Statuseffekt_Verflucht;
    [SerializeField] private GameObject Statuseffekt_Rage;
    [SerializeField] private GameObject Statuseffekt_Stun;
    [SerializeField] private GameObject Statuseffekt_Holy;
    [SerializeField] private GameObject Statuseffekt_Blutend;
    [SerializeField] private GameObject Statuseffekt_Vergifted;
    [SerializeField] private GameObject Statuseffekt_Verbrannt;



    private void Start()
    {
        MainButton();
        gm = FindAnyObjectByType<GM>();

        CheckItems();
    }

    public void Update()
    {
        Statuseffekt_Blutend.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Blutend));
        Statuseffekt_Verbrannt.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Brennend));
        Statuseffekt_Stun.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Gelähmt));
        Statuseffekt_Holy.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Gesegnet));
        Statuseffekt_Verflucht.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Verflucht));
        Statuseffekt_Vergifted.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Vergiftet));
        Statuseffekt_Rage.SetActive(gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Wütend));
    }

    public void MainButton()
    {
        if(mainButtons && attackButtons && itemButtons)
        {
            mainButtons.SetActive(true);
            attackButtons.SetActive(false);
            itemButtons.SetActive(false);
        }
    }
    public void AttackButtons()
    {
        if(mainButtons && attackButtons && itemButtons)
        {
            mainButtons.SetActive(false);
            attackButtons.SetActive(true);
            itemButtons.SetActive(false);
        }
    }
    public void ItemButtons()
    {
        if(mainButtons && attackButtons && itemButtons)
        {
            mainButtons.SetActive(false);
            attackButtons.SetActive(false);
            itemButtons.SetActive(true);
        }         
    }


    public void TurnChange(bool i)
    {
        MainButton();
        if(mainButtons)
        {
            mainButtons.GetComponent<MainButtonScript>().SetMainButtonActive(i);
        }
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
