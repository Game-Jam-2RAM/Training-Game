using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
   private TextMeshProUGUI DiamondText;

    // Start is called before the first frame update
    void Start()
    {
        DiamondText= GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void UpdateDiamondText(PlayerInventory playerInventory)
    {
        DiamondText.text=playerInventory.NumberOfDiamonds.ToString();
   
        
    }
}
