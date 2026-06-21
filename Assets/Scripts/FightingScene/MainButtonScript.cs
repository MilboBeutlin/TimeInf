using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class MainButtonScript : MonoBehaviour
{
    private ButtonManager bM;
    private GM GM;

    [SerializeField] private GameObject Attack1;
    [SerializeField] private GameObject Attack2;
    [SerializeField] private GameObject Attack3;
    [SerializeField] private GameObject Attack4;
    [SerializeField] private GameObject Attack5;
    [SerializeField] private GameObject Attack6;

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
        if(Attack1 != null)
        {
            if(GM.givePlayerAttack(7) != Attacks.NULL && FirstAttackPage == false)
            {
                Attack6.GetComponent<Text>().text = "NEXT";
                Attack1.GetComponent<Text>().text = GM.givePlayerAttack(5).ToString();
                Attack2.GetComponent<Text>().text = GM.givePlayerAttack(6).ToString();
                Attack3.GetComponent<Text>().text = GM.givePlayerAttack(7).ToString();
                Attack4.GetComponent<Text>().text = GM.givePlayerAttack(8).ToString();
                Attack5.GetComponent<Text>().text = GM.givePlayerAttack(9).ToString();

            } else if(GM.givePlayerAttack(7) != Attacks.NULL && FirstAttackPage == true)
            {
                Attack6.GetComponent<Text>().text = "NEXT";
                Attack1.GetComponent<Text>().text = GM.givePlayerAttack(0).ToString();
                Attack2.GetComponent<Text>().text = GM.givePlayerAttack(1).ToString();
                Attack3.GetComponent<Text>().text = GM.givePlayerAttack(2).ToString();
                Attack4.GetComponent<Text>().text = GM.givePlayerAttack(3).ToString();
                Attack5.GetComponent<Text>().text = GM.givePlayerAttack(4).ToString();

            } else 
            {
                Attack1.GetComponent<Text>().text = GM.givePlayerAttack(0).ToString();
                Attack2.GetComponent<Text>().text = GM.givePlayerAttack(1).ToString();
                Attack3.GetComponent<Text>().text = GM.givePlayerAttack(2).ToString();
                Attack4.GetComponent<Text>().text = GM.givePlayerAttack(3).ToString();
                Attack5.GetComponent<Text>().text = GM.givePlayerAttack(4).ToString();
                Attack6.GetComponent<Text>().text = GM.givePlayerAttack(5).ToString();
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

    
}
