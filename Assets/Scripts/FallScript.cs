using UnityEngine;

public class FallScript : MonoBehaviour
{
    // Use this if you want to set position manually instead of via Inspector
    private Vector3 respawnPosition = new Vector3(2f, 0.862f, 5.057f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport player to the respawn point
            other.transform.position = respawnPosition;

            // Stop falling velocity
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }

            Debug.Log("Player fell and was reset.");
        }
    }
}
