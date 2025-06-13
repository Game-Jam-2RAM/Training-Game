using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 10f; // Set initial health value
        slider.maxValue = 100f; // Set maximum health value
        slider.minValue = 0f; // Set minimum health value
        slider.wholeNumbers = true; // Ensure health is displayed as whole numbers
        slider.gameObject.SetActive(true); // Ensure the health bar is visible
    }

    public void UpdateHealth(float healthDifference)
    {
        slider.value += healthDifference; // Update the health bar value
    }
}
