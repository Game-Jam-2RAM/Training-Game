using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds { get; private set; } = 0;
    public InventoryUI inventoryUI; 

    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        Debug.Log("Diamond collected. Total: " + NumberOfDiamonds);

        // Update UI
        if (inventoryUI != null)
        {
            inventoryUI.UpdateDiamondText(this);
        }
        else
        {
            Debug.LogWarning("InventoryUI not assigned to PlayerInventory!");
        }
    }

    public bool SpendDiamond(int amount)
    {
        if (NumberOfDiamonds >= amount)
        {
            NumberOfDiamonds -= amount;

            // Update UI
            if (inventoryUI != null)
            {
                inventoryUI.UpdateDiamondText(this);
            }

            return true;
        }

        return false;
    }
}
