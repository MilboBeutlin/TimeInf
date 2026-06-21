using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM_Game : MonoBehaviour
{

    [SerializeField] private GameObject textFeld;
    [SerializeField] private Text text;
    [SerializeField] private Model model;
    [SerializeField] private Controller controller;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera_Game camera;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject itemGotMssg;
    
    [SerializeField] private Text textItem;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*model = FindAnyObjectByType<Model>();
        controller = FindAnyObjectByType<Controller>();*/
        textFeld.SetActive(false);
        pauseMenu.SetActive(false);
        
        player.transform.position = model.GetSpawnPosition();
        camera.UpdateCamera(model.GetSavePlayerLocation());
        Debug.Log(model.GetSpawnPosition());
        Debug.Log(model.GetSavePlayerLocation());
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
    public string GetText()
    {
        return text.text;
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
        controller.RemoveItem(item,amount);
    }

    public void PlayerDeath()
    {
        Debug.Log("You are dead bitch");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ItemsGot(Items item, int amount)
    {
        itemGotMssg.SetActive(true);
        textItem.text = "You got " + amount + " " + item;
        Invoke("Deactivation", 1f);
    }
    private void Deactivation()
    {
        itemGotMssg.SetActive(false);
    }
}
