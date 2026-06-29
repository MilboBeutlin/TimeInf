
using UnityEngine;
using UnityEngine.UI;

public class bookTutorial_Game : MonoBehaviour
{
    [TextArea(5,20)]
    [SerializeField] private string buchText;

    [SerializeField] private GameObject bookPanel;
    [SerializeField] private Text textFeld;
    private bool mausGedrueckt = false;


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
        if(mausGedrueckt)
        {
            TextAnzeigen();
        }
    }

    private void TextAnzeigen()
    {
        bookPanel.SetActive(true);
        textFeld.text = buchText;
        Time.timeScale = 0f;
    }

    public void TextSchliessen()
    {
        bookPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}