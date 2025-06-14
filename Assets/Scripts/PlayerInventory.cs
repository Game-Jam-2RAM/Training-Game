using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds { get; private set; } = 0;
    public InventoryUI inventoryUI;

    [Header("Audio Settings")]
    public AudioClip collectSound; // Assign the sound clip in the Inspector
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        Debug.Log("Diamond collected. Total: " + NumberOfDiamonds);

        // Play collection sound
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        else
        {
            Debug.LogWarning("Collect sound or AudioSource not set.");
        }

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
