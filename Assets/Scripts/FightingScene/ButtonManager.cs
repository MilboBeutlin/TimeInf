using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject mainButtons;

    [SerializeField] private GameObject attackButtons;

    private GM gm;

    [SerializeField] private GameObject itemButtons;
    [SerializeField] private Transform canvas;

    [SerializeField] private GameObject[] Itembuttons;

    private void Start()
    {
        MainButton();
        gm = FindAnyObjectByType<GM>();
    }
    
    public void MainButton()
    {
        mainButtons.SetActive(true);
        attackButtons.SetActive(false);
        itemButtons.SetActive(false);
    }
    public void AttackButtons()
    {
        mainButtons.SetActive(false);
        attackButtons.SetActive(true);
        itemButtons.SetActive(false);
    }
    public void ItemButtons()
    {
        mainButtons.SetActive(false);
        attackButtons.SetActive(false);
        itemButtons.SetActive(true);
    }

    private void Update()
    {
        
    }

    

    public void TurnChange(bool i)
    {
        MainButton();
        mainButtons.GetComponent<MainButtonScript>().SetMainButtonActive(i);
    }

  


}
