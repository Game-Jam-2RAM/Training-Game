using UnityEngine;

public class PonusRoad : MonoBehaviour
{
    public GameObject ponusRoad;         // Assign in Inspector
    public InventoryUI inventoryUI;      // Assign in Inspector
    public int diamondCost = 4;          // How many diamonds needed to activate the road

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogError("PlayerInventory not found on Player.");
            return;
        }

        if (inventory.SpendDiamond(diamondCost))
        {
            // if (inventoryUI != null)
            // {
            //     inventoryUI.UpdateDiamondText(inventory);
            // }

            if (ponusRoad != null && !ponusRoad.activeInHierarchy)
            {
                ponusRoad.SetActive(true);
                Debug.Log("Ponus road activated.");
            }
        }
        else
        {
            Debug.Log("Not enough diamonds to activate Ponus road.");
        }
    }
}
