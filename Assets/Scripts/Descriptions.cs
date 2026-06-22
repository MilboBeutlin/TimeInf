using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Description : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Text descriptionText;
    [SerializeField] private string description;
    [Header("OnlyForAttacksButtons:")]
    [SerializeField] private Text ownText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true); 
        if(ownText == null)
        {
            descriptionText.text = description;
        }
        else if (Attacks.TryParse<Attacks>(ownText.text, out Attacks result))
        {
            descriptionText.text = result switch
            {
            Attacks.Protection => "Deals 50 damage",
            Attacks.Hammer => "Deals 500 damage",
            Attacks.Mutilation => "Deals 5 damage",
            Attacks.Strike => "Deals 6 damage",
            Attacks.PoisonDagger => "Deals 9 damage",
            Attacks.Fireball => "Deals 20 damage",
            Attacks.Dampen => "Deals 55 damage",
            Attacks.Vengeance => "Deals 250 damage",
            Attacks.Dig => "Deals 1 damage",
            Attacks.LightOfHope => "Deals 500 damage",
            Attacks.RedeemingStrike => "Deals 500 damage",
            Attacks.Cleansing => "Deals 500 damage",
            Attacks.AgonyStrike => "Deals 500 damage",
            Attacks.Enlightenment => "Deals 500 damage",
            Attacks.Swim => "Deals 500 damage",
            Attacks.NULL => "You don't have that attack yet",
            _ => "error"
            };
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionPanel.SetActive(false);
    }
    private void OnDisable()
    {
        descriptionPanel.SetActive(false);
    }
   /* void OnMouseEnter()
    {
        descriptionPanel.SetActive(true);
        descriptionText.text = description;
    }

    void OnMouseExit()
    {
        descriptionPanel.SetActive(false);
    }*/
}
