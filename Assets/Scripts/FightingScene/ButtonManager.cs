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
        
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Blutend))
        {
            Statuseffekt_Blutend.SetActive(true);
        } else
        {
            Statuseffekt_Blutend.SetActive(false);
        }
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Brennend))
        {
            Statuseffekt_Verbrannt.SetActive(true);
        }
        else
        {
            Statuseffekt_Verbrannt.SetActive(false);
        }
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Gelähmt))
        {
            Statuseffekt_Stun.SetActive(true);
        }
        else
        {
            Statuseffekt_Stun.SetActive(false);
        }
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Gesegnet))
        {
            Statuseffekt_Holy.SetActive(true);
        }
        else
        {
            Statuseffekt_Holy.SetActive(false);
        }
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Verflucht))
        {
            Statuseffekt_Verflucht.SetActive(true);
        }
        else
        {
            Statuseffekt_Verflucht.SetActive(false);
        }
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Vergiftet))
        {
            Statuseffekt_Vergifted.SetActive(true);
        }
        else
        {
            Statuseffekt_Vergifted.SetActive(false);
        }
        if (gm.giveCurrentPlayerEffects().ContainsKey(Statuseffekte.Wütend))
        {
            Statuseffekt_Rage.SetActive(true);
        }
        else
        {
            Statuseffekt_Rage.SetActive(false);
        }

    }
    //refactor idea statt deiner update, wird immer aufgerufen wenn actually effekte sich ändern:
    public void UpdateStatusffekt(Dictionary<Statuseffekte, int> StatusEffekte)
    {
        Statuseffekt_Blutend.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Blutend));
        Statuseffekt_Verbrannt.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Brennend));
        Statuseffekt_Stun.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Gelähmt));
        Statuseffekt_Holy.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Gesegnet));
        Statuseffekt_Verflucht.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Verflucht));
        Statuseffekt_Vergifted.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Vergiftet));
        Statuseffekt_Rage.SetActive(StatusEffekte.ContainsKey(Statuseffekte.Wütend));
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
