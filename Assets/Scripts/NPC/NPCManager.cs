using UnityEngine;
using System.Collections.Generic;

public class NPCManager : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array to hold the customer prefabs
    public Transform spawnPoint; // Where to spawn the customers
    public float spawnInterval = 2.0f; // Time interval between customer spawns
    public int maxCustomers = 4; // Maximum number of customers allowed
    private List<GameObject> activeCustomers = new List<GameObject>(); // Track active customers
    private List<GameObject> inactiveCustomers = new List<GameObject>(); // Track inactive customers

    void Start()
    {
        // Initialize inactive customers
        InitializeInactiveCustomers();

        StartCoroutine(SpawnCustomers());
    }

    private void InitializeInactiveCustomers()
    {
        foreach (var prefab in customerPrefabs)
        {
            GameObject customer = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            customer.SetActive(false); // Set them as inactive
            inactiveCustomers.Add(customer); // Add to the inactive list
        }
    }

    private System.Collections.IEnumerator SpawnCustomers()
    {
        while (activeCustomers.Count < maxCustomers && inactiveCustomers.Count > 0)
        {
            // Randomly choose one of the inactive customer GameObjects to activate
            int randomIndex = Random.Range(0, inactiveCustomers.Count);
            GameObject customer = inactiveCustomers[randomIndex];

            customer.SetActive(true); // Activate the customer
            activeCustomers.Add(customer); // Add the customer to the list of active customers
            inactiveCustomers.RemoveAt(randomIndex); // Remove from the inactive list

            yield return new WaitForSeconds(spawnInterval); // Wait for the next spawn
        }
    }

    // Method to remove a customer from the active list
    public void RemoveCustomer(GameObject customer)
    {
        if (activeCustomers.Contains(customer))
        {
            activeCustomers.Remove(customer); // Remove from active list
            inactiveCustomers.Add(customer); // Add to inactive list
            customer.SetActive(false); // Deactivate the customer
        }
    }
}
