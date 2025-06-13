using UnityEngine;
using System.Collections.Generic;

public class SymbolRandomizer : MonoBehaviour
{
    public GameObject[] symbolPrefabs;     // Your symbol prefabs
    public Transform[] symbolPositions;    // Possible positions to place them
    public Color keyColor = Color.yellow;  // The color for the key symbol
    public int keySymbolIndex;             // Index

    void Start()
    {
        if (symbolPrefabs.Length != symbolPositions.Length)
        {
            Debug.LogError("The number of symbols and positions must match!");
            return;
        }

        // Choose one random index to highlight
        int keySymbolIndex = Random.Range(0, symbolPrefabs.Length);

        // Convert positions to a list we can shuffle
        List<Transform> shuffledPositions = new List<Transform>(symbolPositions);

        // Shuffle the position list
        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            int rnd = Random.Range(i, shuffledPositions.Count);
            (shuffledPositions[i], shuffledPositions[rnd]) = (shuffledPositions[rnd], shuffledPositions[i]);
        }

        // Assign each symbol to a shuffled position

        GameObject symbolInstance = Instantiate(
            symbolPrefabs[1],
            shuffledPositions[1].position,
            shuffledPositions[1].rotation,
            transform);
           

            // // Highlight the chosen symbol
            // if (i == keySymbolIndex)
            // {
            //     Renderer rend = symbolInstance.GetComponentInChildren<Renderer>();
            //     if (rend != null)
            //     {
            //         rend.material.color = keyColor;
            //     }
            // }
       // }
    }
}