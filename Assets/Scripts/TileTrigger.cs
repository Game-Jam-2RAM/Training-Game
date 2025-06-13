using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    private TilePuzzleManager puzzleManager;
    private Renderer rend;

    void Start()
    {
        puzzleManager = FindObjectOfType<TilePuzzleManager>();
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleManager.PlayerSteppedOnTile(gameObject);
            Debug.Log("hello my sweetie!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (rend != null)
            {
                rend.material = puzzleManager.normalMaterial;
                Debug.Log("Player left tile: material reset.");
            }
        }
    }
}
