using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array to hold the customer prefabs
    public GameObject[] availableSeats;
    public Transform spawnPoint; // Where to spawn the customers
    public float spawnInterval = 2.0f; // Time interval between customer spawns
    private List<GameObject> activeCustomers = new List<GameObject>(); // Track active customers
    private List<GameObject> inactiveCustomers = new List<GameObject>(); // Track inactive customers

    void Start()
    {
        // Initialize inactive customers only once at the start
        InitializeInactiveCustomers();
        StartCoroutine(SpawnCustomers());
    }

    private void InitializeInactiveCustomers()
    {
        for (int i = 0; i < availableSeats.Length; i++)
        {
            Debug.Log("generate " + i);
            GameObject randomCustomer = generateRandomCustomer();

            npcAI npcAi = randomCustomer.GetComponent<npcAI>();

            GameObject sitPoint = availableSeats[i].transform.Find("sitPoint").gameObject;

            npcAi.SetSitPoint(sitPoint);

            GameObject customer = Instantiate(randomCustomer, spawnPoint.position, Quaternion.identity);
            customer.SetActive(false); // Set them as inactive
            inactiveCustomers.Add(customer); // Add to the inactive list
        }
    }

    private GameObject generateRandomCustomer() {
        return customerPrefabs[Random.Range(0, customerPrefabs.Length)];
    }

    private System.Collections.IEnumerator SpawnCustomers()
    {
        // Loop to spawn customers until maxCustomers is reached
        for (int i = 0; i < availableSeats.Length; i++)
        {
            Debug.Log("spawn " + i);
            GameObject customer = inactiveCustomers[i];

            customer.transform.position = spawnPoint.position; // Reset position
            customer.SetActive(true); // Activate the customer
            activeCustomers.Add(customer); // Add to the list of active customers

            yield return new WaitForSeconds(spawnInterval); // Wait before next spawn
        }

        inactiveCustomers.Clear(); // Remove from inactive list
    }

    // Method to remove a customer from the active list and deactivate them
    // public void RemoveCustomer(GameObject customer)
    // {
    //     if (activeCustomers.Contains(customer))
    //     {
    //         activeCustomers.Remove(customer); // Remove from active list
    //         inactiveCustomers.Add(customer); // Add back to inactive list
    //         customer.SetActive(false); // Deactivate the customer
    //     }
    // }

    public void GenerateCustomer()
    {
        activeCustomers.Clear();
        inactiveCustomers.Clear();

        StartCoroutine(SpawnCustomers());
    }
}
