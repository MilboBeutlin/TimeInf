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
        //descriptionPanel.SetActive(true); 
        if(ownText == null)
        {
            descriptionText.text = description;
        }
        else if (Attacks.TryParse<Attacks>(ownText.text, out Attacks result))
        {
            descriptionText.text = result switch
            {
            Attacks.Protection => "Protects you for a turn",
            Attacks.Hammer => "Deals medium damage",
            Attacks.Mutilation => "Deals high damage. Small chance to inflict bleeding",
            Attacks.Strike => "Deals small damage. Medium Chance to stun",
            Attacks.PoisonDagger => "Deals small damage. Inflicts poison. Small chance to inflict bleeding",
            Attacks.Fireball => "Deals medium damage. Inflict burning",
            Attacks.Dampen => "Weakens the enemy",
            Attacks.Vengeance => "Can deal between small and very high damage.",
            Attacks.Dig => "Deals small damage. Ignores Armor.",
            Attacks.LightOfHope => "You gain the buff hopefull",
            Attacks.RedeemingStrike => "Deals medium damage. Removes poison and inflicts extra small damage",
            Attacks.Cleansing => "Removes all Debuffs from you",
            Attacks.AgonyStrike => "Sacrifice LP to deal high damage",
            Attacks.Enlightenment => "You will see the end of the fight. A void",
            Attacks.Swim => "There is no water so don't try to swim",
            Attacks.NULL => "You don't have that attack yet",
            _ => "error"
            };
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //descriptionPanel.SetActive(false);
        descriptionText.text = "";
    }
    private void OnDisable()
    {
        //descriptionPanel.SetActive(false);
        descriptionText.text = "";
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
