using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public TilePuzzleManager puzzleManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            puzzleManager.StartPuzzle();
            gameObject.SetActive(false); // Disable trigger so it doesn’t run again
        }
    }
}
