using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Displays a description when the user hovers over a UI element.
public class Description : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Text descriptionText;
    [SerializeField] private string description;

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionPanel.SetActive(true);
        descriptionText.text = description;
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
