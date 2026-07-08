using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;

public class MainButtonScript : MonoBehaviour
{
    private ButtonManager bM;
    private GM GM;

    [SerializeField] private Text[] AttacksArray;

    [SerializeField] private GameObject ItemButtons;

    [SerializeField] private Items ButtonType;
    [SerializeField] private Text ItemButtonText;

    private bool FirstAttackPage = true;
    [SerializeField] private Transform attackButtonsParent;
//private TextMeshProUGUI[] AttacksArray;

void Awake()
{
    if(attackButtonsParent != null){
        AttacksArray = attackButtonsParent.GetComponentsInChildren<Text>(true);
    }
}

    void Start()
    {
        bM = GetComponentInParent<ButtonManager>();
        GM = FindAnyObjectByType<GM>();
    }


    public void Update()
    {
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

    

    // F�r Unity Buttons, um Attacken auszuf�hren
    
    /*
    public async void DoUseItem(int r)
    {
        Items i = (Items)r;
        await GM.DoUseItem(i);
    }
    */

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
