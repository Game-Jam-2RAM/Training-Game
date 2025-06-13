using UnityEngine;
using System.Collections.Generic;

public class SymbolRandomizer : MonoBehaviour
{
    public GameObject[] symbolPrefabs;     // Your symbol prefabs
    public Transform[] symbolPositions;    // Possible positions to place them

    void Start()
    {
        if (symbolPrefabs.Length != symbolPositions.Length)
        {
            Debug.LogError("The number of symbols and positions must match!");
            return;
        }

        // Convert positions to a list we can shuffle
        List<Transform> shuffledPositions = new List<Transform>(symbolPositions);

        // Shuffle the position list
        for (int i = 0; i < shuffledPositions.Count; i++)
        {
            int rnd = Random.Range(i, shuffledPositions.Count);
            (shuffledPositions[i], shuffledPositions[rnd]) = (shuffledPositions[rnd], shuffledPositions[i]);
        }

        // Assign each symbol to a shuffled position
        for (int i = 0; i < symbolPrefabs.Length; i++)
        {
            Instantiate(symbolPrefabs[i], shuffledPositions[i].position, shuffledPositions[i].rotation, transform);
        }
    }
}
