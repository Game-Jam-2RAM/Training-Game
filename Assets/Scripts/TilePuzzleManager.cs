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
    public float delayBeforePlayerMove = 3f;

    public Material normalMaterial;
    public Material lightMaterial;
    public Material correctTileMaterial; // Green
    public Material wrongTileMaterial;   // Red
    public HealthScript health;
    public AudioClip damageSound;
    private AudioSource audioSource;

    public GameObject entranceBarrier;   // Invisible wall to prevent early access

    // New damage variable:
    public int wrongTileDamage = 10;

    void Start()
    {
        AssignTiles();
        GenerateRandomPath();
        //StartCoroutine(ShowCorrectPath());
        if (health == null)
            health = FindObjectOfType<HealthScript>();
        audioSource = GetComponent<AudioSource>();
    }
    public void StartPuzzle()
{
        
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
        for (int i = 0; i < rows; i++)
        {
            LightUpTile(i, correctPath[i]);
            yield return new WaitForSeconds(lightUpTime);
            TurnOffTile(i, correctPath[i]);
        }

        // Remove the invisible barrier
        if (entranceBarrier != null)
            entranceBarrier.SetActive(false);
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

    // Call this when the player steps on a tile
    public void PlayerSteppedOnTile(GameObject tile)
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                if (tiles[row, col] == tile)
                {
                    Renderer rend = tile.GetComponent<Renderer>();

                    if (correctPath[row] == col)
                    {
                        rend.material = correctTileMaterial;
                        Debug.Log("Correct tile!");
                    }
                    else
                    {
                        rend.material = wrongTileMaterial;
                        Debug.Log("Wrong tile!");

                        // 💥 Damage the player here!
                        if (health != null)
                        {
                            health.UpdateHealth(-wrongTileDamage);
                            if (damageSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(damageSound);
                    }
                        }
                    }

                    return;
                }
            }
        }
    }
}
