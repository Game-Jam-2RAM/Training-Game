using UnityEngine;

public class Gate : MonoBehaviour
{
    public int symbolIndex; // Set this to match the symbol index

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int correctIndex = FindObjectOfType<SymbolRandomizer>().keySymbolIndex;

            if (symbolIndex == correctIndex)
            {
                Debug.Log("✅ Correct Gate! You win!");
                // Optional: Add animation, sound, next level, etc.
            }
            else
            {
                Debug.Log("❌ Wrong Gate! Try again!");
                // Optional: Play fail sound, reset player, etc.
            }
        }
    }
}