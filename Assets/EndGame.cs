using UnityEngine;

public class EndGame : MonoBehaviour
{
    // end the game if gameObject with tag player collides with this object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.ChangeScene("FinalStageMenu");
        }
    }
}
