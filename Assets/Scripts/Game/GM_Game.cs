using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GM_Game : MonoBehaviour
{

    [SerializeField] private GameObject textFeld;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Model model;
    [SerializeField] private Controller con;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        model = FindAnyObjectByType<Model>();
        con = FindAnyObjectByType<Controller>();
        textFeld.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeText(string Itext)
    {
        //text.GetComponent<TMP_Text>().text = Itext;
        text.text = Itext;
    }
    public void ShowText(bool i)
    {
        if( i == true)
        {
            textFeld.SetActive(true);
        }
        else
        {
            textFeld.SetActive(false);
        }
    }

    public Dictionary<Items, int> giveCurrentPlayerItems()
    {
        return model.GetCurrentPlayerItems();
    }

    public void RemoveItem(Items item, int amount)
    {
        con.RemoveItem(item,amount);
    }

}
