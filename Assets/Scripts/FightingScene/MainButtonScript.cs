using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

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



    void Start()
    {
        bM = GetComponentInParent<ButtonManager>();
        GM = FindAnyObjectByType<GM>();
    }

    public void Attacks()
    {
        bM.Attacks();
        Destroy(this.gameObject);
    }

    public void MainButtons()
    {
        bM.MainButtons();
        Destroy(this.gameObject);
    }

    public void Items()
    {
        bM.Items();
        Destroy(this.gameObject);
    }

    public void Update()
    {
        if(Attack1 != null)
        {
            Attack1.GetComponent<TMP_Text>().text = GM.givePlayerAttack(0).ToString();
            Attack2.GetComponent<TMP_Text>().text = GM.givePlayerAttack(1).ToString();
            Attack3.GetComponent<TMP_Text>().text = GM.givePlayerAttack(2).ToString();
            Attack4.GetComponent<TMP_Text>().text = GM.givePlayerAttack(3).ToString();
            Attack5.GetComponent<TMP_Text>().text = GM.givePlayerAttack(4).ToString();
            Attack6.GetComponent<TMP_Text>().text = GM.givePlayerAttack(5).ToString();
            
        }
        
    }

    

    public void DeactivateButtons()
    {
        if (GM.GetPlayerturn() == false)
        {
            AttackButtons.GetComponent<Button>().interactable = false;
            ItemButtons.GetComponent<Button>().interactable = false;
            FleeButtons.GetComponent<Button>().interactable = false;
        }

    }

    public void ActivateButtons()
    {
        
            AttackButtons.GetComponent<Button>().interactable = true;
            ItemButtons.GetComponent<Button>().interactable = true;
            FleeButtons.GetComponent<Button>().interactable = true;
        
    }
    

    public void DoAttack_ONE()
    {
        GM.DoAttack(0);
    }
    public void DoAttack_TWO()
    {
        GM.DoAttack(1);
    }
    public void DoAttack_THREE()
    {
        GM.DoAttack(2);
    }
    public void DoAttack_FOUR()
    {
        GM.DoAttack(3);
    }
    public void DoAttack_FIVE()
    {
        GM.DoAttack(4);
    }
    public void DoAttack_SIX()
    {
        GM.DoAttack(5);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
