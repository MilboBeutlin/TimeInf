using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Displays a description when the user hovers over a UI element.
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
        if (ownText == null)
        {
            descriptionText.text = description;
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
}
