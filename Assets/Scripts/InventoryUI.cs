using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class InventoryUI : MonoBehaviour
{
    public TextMeshProUGUI diamondText;
    

    public void UpdateDiamondText(PlayerInventory inventory)
    {
        diamondText.text = inventory.NumberOfDiamonds.ToString();
   
        
    }
}
