
using UnityEngine;
using UnityEngine.UI;

public class bookTutorial_Game : MonoBehaviour
{
    [TextArea(5,20)]
    [SerializeField] private string buchText;

    [SerializeField] private GameObject bookPanel;
    [SerializeField] private Text textFeld;

    private bool playerInRange = false;
    private bool mausGedrueckt = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
      
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    
  
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

     private void OnMouseDown()
    {
        mausGedrueckt = true;
    }

    private void OnMouseUp()
    {
          mausGedrueckt = false;
    }

    private void Update()
    {
        if(playerInRange && mausGedrueckt)
        {
            TextAnzeigen();
        }
    }

    private void TextAnzeigen()
    {
        bookPanel.SetActive(true);
        textFeld.text = buchText;
    }

    public void TextSchliessen()
    {
        bookPanel.SetActive(false);
    }
}