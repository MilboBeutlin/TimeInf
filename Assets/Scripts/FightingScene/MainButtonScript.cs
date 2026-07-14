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


    [SerializeField] private GameObject ItemButtons;

    [SerializeField] private Items ButtonType;
    [SerializeField] private Text ItemButtonText;

    [SerializeField] private Button StrikeButton;
    [SerializeField] private Button CounterButton;
    [SerializeField] private Button ItemButton;

    
//private TextMeshProUGUI[] AttacksArray;



    void Start()
    {
        bM = GetComponentInParent<ButtonManager>();
        GM = FindAnyObjectByType<GM>();

        if(GM.giveCurrentPlayerItems().Count <= 0)
        {
            ItemButton.interactable = false;
        }
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

    

    
    public void DoUseItem(int r)
    {
        Items i = (Items) r;
        GM.DoUseItem(i);
    }
    

    public void Counter()
    {
        GM.Counter();
    }

    public void Striker()
    {
        GM.Strike();
    }

    public void EnableButtons(bool i)
    {
        StrikeButton.interactable = i;
        CounterButton.interactable = i;
        if (GM.giveCurrentPlayerItems().Count >= 1)
        {
            ItemButton.interactable = i;
        }
    }
    
}
