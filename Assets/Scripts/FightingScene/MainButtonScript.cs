using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class MainButtonScript : MonoBehaviour
{
    private ButtonManager bM;
    private GM GM;

    [SerializeField] private GameObject[] AttacksArray;

    [SerializeField] private GameObject AttackButtons;
    [SerializeField] private GameObject ItemButtons;
    [SerializeField] private GameObject FleeButtons;

    [SerializeField] private Items ButtonType;
    [SerializeField] private Text ItemButtonText;

    private bool FirstAttackPage = true;
    

    void Start()
    {
        bM = GetComponentInParent<ButtonManager>();
        GM = FindAnyObjectByType<GM>();
    }


    public void Update()
    {
        
            if(GM.givePlayerAttack(7) != Attacks.NULL && FirstAttackPage == false)
            {
                
                AttacksArray[0].GetComponent<Text>().text = GM.givePlayerAttack(5).ToString();
                AttacksArray[1].GetComponent<Text>().text = GM.givePlayerAttack(6).ToString();
                AttacksArray[2].GetComponent<Text>().text = GM.givePlayerAttack(7).ToString();
                AttacksArray[3].GetComponent<Text>().text = GM.givePlayerAttack(8).ToString();
                AttacksArray[4].GetComponent<Text>().text = GM.givePlayerAttack(9).ToString();
                AttacksArray[5].GetComponent<Text>().text = "NEXT";

            } else if(GM.givePlayerAttack(7) != Attacks.NULL && FirstAttackPage == true)
            {
                
                AttacksArray[0].GetComponent<Text>().text = GM.givePlayerAttack(0).ToString();
                AttacksArray[1].GetComponent<Text>().text = GM.givePlayerAttack(1).ToString();
                AttacksArray[2].GetComponent<Text>().text = GM.givePlayerAttack(2).ToString();
                AttacksArray[3].GetComponent<Text>().text = GM.givePlayerAttack(3).ToString();
                AttacksArray[4].GetComponent<Text>().text = GM.givePlayerAttack(4).ToString();
                AttacksArray[5].GetComponent<Text>().text = "NEXT";

            } else 
            {
                AttacksArray[0].GetComponent<Text>().text = GM.givePlayerAttack(0).ToString();
                AttacksArray[1].GetComponent<Text>().text = GM.givePlayerAttack(1).ToString();
                AttacksArray[2].GetComponent<Text>().text = GM.givePlayerAttack(2).ToString();
                AttacksArray[3].GetComponent<Text>().text = GM.givePlayerAttack(3).ToString();
                AttacksArray[4].GetComponent<Text>().text = GM.givePlayerAttack(4).ToString();
                AttacksArray[5].GetComponent<Text>().text = GM.givePlayerAttack(5).ToString();
            }
        
        for (int g = 0; g < AttacksArray.Length; g++) 
        {
            if (g >= 5)
            {
                g -= 5;
            }
            if (GM.givePlayerAttack(g) == Attacks.NULL) 
            {

                AttacksArray[g].SetActive(false);
            } else
            {

                AttacksArray[g].SetActive(true);
            }
            if (g >= 5)
            {
                g += 5;
            }
        }

        
        
        if (ItemButtonText != null)
        {
            ItemButtonText.text = ButtonType.ToString();
        }
    }

    public Items giveItemButtonType()
    {
        return ButtonType;
    }

    public void setItemButtonType(Items i)
    {
        i = ButtonType;
    }

    public void SetMainButtonActive(bool active)
    {
        if(active == true)
        {
            AttackButtons.GetComponent<Button>().interactable = true;
            ItemButtons.GetComponent<Button>().interactable = true;
            FleeButtons.GetComponent<Button>().interactable = true;
        } else if (active == false)
        {
            AttackButtons.GetComponent<Button>().interactable = false;
            ItemButtons.GetComponent<Button>().interactable = false;
            FleeButtons.GetComponent<Button>().interactable = false;
        }
    }

    // F�r Unity Buttons, um Attacken auszuf�hren
    public void DoAttack(int i)
    {
        if (i == 5 && GM.givePlayerAttack(7) != Attacks.NULL)
        {
            FirstAttackPage = !FirstAttackPage;
        } else if (FirstAttackPage == false && GM.givePlayerAttack(7) != Attacks.NULL)
        {
            GM.DoAttack(i +5);
        } else {
            GM.DoAttack(i);
        }
    }
    public void DoUseItem(int r)
    {
        Items i = (Items)r;
        GM.DoUseItem(i);
    }

    public void SwitchAttackPages()
    {
        if(FirstAttackPage == true)
        {
            FirstAttackPage = false;
        } else
        {
            FirstAttackPage = true;
        }
    }


    public void Analyse()
    {
        GM.Analyse();
        
    }
    
}
