using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePuzzleManager : MonoBehaviour
{
    public int rows = 7;
    public int columns = 4;

    private int[] correctPath;
    private GameObject[,] tiles;

    public float lightUpTime = 2f;
    public float delayBeforePlayerMove = 2f;
    public Material normalMaterial;
public Material lightMaterial;

    void Start()
    {
        AssignTiles();
        GenerateRandomPath();
        StartCoroutine(ShowCorrectPath());
    }

    void AssignTiles()
    {
        tiles = new GameObject[rows, columns];
        int index = 0;

        foreach (Transform child in transform)
        {
            int row = index / columns;
            int col = index % columns;
            tiles[row, col] = child.gameObject;
            index++;
        }
    }

    void GenerateRandomPath()
    {
        correctPath = new int[rows];
        correctPath[0] = Random.Range(0, columns);

        for (int i = 1; i < rows; i++)
        {
            int previousColumn = correctPath[i - 1];
            List<int> possibleColumns = new List<int>();

            if (previousColumn > 0)
                possibleColumns.Add(previousColumn - 1);
            possibleColumns.Add(previousColumn);
            if (previousColumn < columns - 1)
                possibleColumns.Add(previousColumn + 1);

            correctPath[i] = possibleColumns[Random.Range(0, possibleColumns.Count)];
        }

        Debug.Log("Generated Path:");
        for (int i = 0; i < rows; i++)
            Debug.Log("Row " + i + " -> Column " + correctPath[i]);
    }

    IEnumerator ShowCorrectPath()
    {
        yield return new WaitForSeconds(delayBeforePlayerMove);

        for (int i = 0; i < rows; i++)
        {
            LightUpTile(i, correctPath[i]);
            yield return new WaitForSeconds(lightUpTime);
            TurnOffTile(i, correctPath[i]);
        }

        Debug.Log("Player can start moving now.");
        // After this you can enable player control
    }

    void LightUpTile(int row, int column)
{
    Renderer rend = tiles[row, column].GetComponent<Renderer>();
    rend.material = lightMaterial;
}

void TurnOffTile(int row, int column)
{
    Renderer rend = tiles[row, column].GetComponent<Renderer>();
    rend.material = normalMaterial;
}

}
