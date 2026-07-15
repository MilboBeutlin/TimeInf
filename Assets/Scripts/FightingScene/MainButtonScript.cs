using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

// Controls the battle UI buttons and forwards button actions to the game manager.
// Also enables or disables buttons depending on the current game state.
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



    void Start()
    {
        bM = GetComponentInParent<ButtonManager>();
        GM = FindAnyObjectByType<GM>();

        // Disable the item button if the inventory is empty.
        if(ItemButton && GM.giveCurrentPlayerItems().Count <= 0)
        {
            ItemButton.interactable = false;
        }
    }


    public void Update()
    {
        // Keeps the displayed item name synchronized with the assigned item type.
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

    

    // Uses the selected inventory item.
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

    //Enables or disables the battle buttons.
    //The item button is only enabled if the player has at least 1 item
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
