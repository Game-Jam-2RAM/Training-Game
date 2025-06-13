using UnityEngine;

public class FallScript : MonoBehaviour
{
    private Vector3 respawnPosition = new Vector3(2f, 0.862f, 5.057f);
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport player to the respawn point
            other.transform.position = respawnPosition;

            // Stop falling velocity
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }

            Debug.Log("Player fell and was reset.");
        }
    }
}
