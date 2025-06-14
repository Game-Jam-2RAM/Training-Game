using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public Slider slider; // Made public so other scripts can access it

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 100f; // Set initial health value
        slider.maxValue = 100f; // Set maximum health value
        slider.minValue = 0f; // Set minimum health value
        slider.wholeNumbers = true; // Ensure health is displayed as whole numbers
        slider.gameObject.SetActive(true); // Ensure the health bar is visible
    }

    public void UpdateHealth(float healthDifference)
    {
        slider.value += healthDifference; // Update the health bar value
        Debug.Log("Health updated by " + healthDifference + ". Current health: " + slider.value);
    }

    // Get current health value
    public float GetCurrentHealth()
    {
        return slider.value;
    }
}
