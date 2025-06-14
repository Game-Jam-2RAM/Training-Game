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
        PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
        if (other.CompareTag("Player") && playerInventory.SpendDiamond(10))
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
