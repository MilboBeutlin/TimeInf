using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering.Universal;

// Main game manager for handling global gameplay functions for the game scene.
// Controls UI messages, player setup, inventory interactions and scene reloads.
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
    [SerializeField] private GameObject spawnPoint;

    void Start()
    {
        // Initializes the player position and camera location from the saved data.
        pauseMenu.SetActive(false);

        player.transform.position = model.GetSpawnPosition();
        spawnPoint.transform.position = model.GetSpawnPosition();
        camera.UpdateCamera(model.GetSavePlayerLocation());

        // Shows a short tutorial message at the start.
        ChangeText("You can click on books to read them.");
        ShowText(true);
        Invoke(nameof(CloseTutorial), 10f);
    }

    private void CloseTutorial()
    {
        if (text.text == "You can click on books to read them.")
        {
            ShowText(false);
        }
    }

    public void ChangeText(string Itext)
    {
        text.text = Itext;
    }

    public string GetText()
    {
        return text.text;
    }

    public void ShowText(bool i)
    {
        if (i == true)
        {
            textFeld?.SetActive(true);
        }
        else
        {
            textFeld?.SetActive(false);
        }
    }

    public Dictionary<Items, int> giveCurrentPlayerItems()
    {
        return model.GetCurrentPlayerItems();
    }

    public void RemoveItem(Items item, int amount)
    {
        controller.RemoveItem(item, amount);
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ItemsGot(Items item, int amount)
    {
        itemGotMssg.SetActive(true);
        textItem.text = "You got " + amount + " " + item;

        Invoke("Deactivation", 1f);
    }

    public void SwimGot()
    {
        itemGotMssg.SetActive(true);
        textItem.text = "You can walk on water";

        Invoke("Deactivation", 1f);
    }

    private void Deactivation()
    {
        itemGotMssg.SetActive(false);
    }

}
