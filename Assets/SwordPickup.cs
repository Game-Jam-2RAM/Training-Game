using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public GameObject player1; // Assign in Inspector
    public GameObject player2; // Assign in Inspector

    public GameObject[] gates;

    private void Start()
    {
        gates = GameObject.FindGameObjectsWithTag("Gate");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(player1);
            player2.SetActive(true);

            Destroy(gameObject);
            foreach (GameObject gate in gates)
            {
                if (gate != null)
                {
                    Destroy(gate);
                }
            }
        }
    }
}
