using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GM_Game : MonoBehaviour
{

    [SerializeField] private GameObject text;
    [SerializeField] private Model model;
    [SerializeField] private Controller con;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        model = FindAnyObjectByType<Model>();
        con = FindAnyObjectByType<Controller>();
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeText(string Itext)
    {
        text.GetComponent<TMP_Text>().text = Itext;
    }
    public void ShowText(bool i)
    {
        if( i == true)
        {
            text.SetActive(true);
        }
        else
        {
            text.SetActive(false);
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
