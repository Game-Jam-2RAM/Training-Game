using UnityEngine;

public class FallScript : MonoBehaviour
{
    private Vector3 respawnPosition = new Vector3(2f, 0.862f, 5.057f);
    public AudioClip fallSound; // Sound to play when the player falls
    private AudioSource audioSource;
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play fall sound if assigned
            if (fallSound != null)
            {
                if (audioSource == null)
                {
                    audioSource = other.GetComponent<AudioSource>();
                    if (audioSource == null)
                    {
                        audioSource = other.gameObject.AddComponent<AudioSource>();
                    }
                }
                audioSource.PlayOneShot(fallSound);
            }
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
