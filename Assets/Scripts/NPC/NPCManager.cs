using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class NPCManager : MonoBehaviour
{
    public GameObject[] customerPrefabs; // Array to hold the customer prefabs
    public GameObject[] availableSeats;
    public Transform spawnPoint; // Where to spawn the customers
    public float spawnInterval = 2.0f; // Time interval between customer spawns
    private List<GameObject> activeCustomers = new List<GameObject>(); // Track active customers
    private List<GameObject> inactiveCustomers = new List<GameObject>(); // Track inactive customers

    public Animator doorAnimator;

    private int servedCustomers = 0;

    void Start()
    {
        // Initialize inactive customers only once at the start
        InitializeInactiveCustomers();
        StartCoroutine(SpawnCustomers());
    }

    public void AddMoreCustomer(GameObject sitPoint)
    {
        StartCoroutine(SpawnACustomer(sitPoint));
    }

    public void ResetServedCustomers()
    {
        servedCustomers = 0;
    }

    public void IncrementServedCustomers()
    {
        servedCustomers++;
    }

    public int GetServedCustomers()
    {
        return servedCustomers;
    }

    private System.Collections.IEnumerator SpawnACustomer(GameObject sitPoint)
    {

        OpenDoor();

        // random delay before spawning the customer
        int randomDelay = Random.Range(1, 5);
        yield return new WaitForSeconds(randomDelay); // Add delay before spawning the customer

        GameObject randomCustomer = generateRandomCustomer();

        NPC_AI npcAI = randomCustomer.GetComponent<NPC_AI>();
        if (npcAI == null)
        {
            // Debug.LogError("NPCAI component not found on customer prefab");
            yield break;
        }

        npcAI.SetSitPoint(sitPoint);

        GameObject customer = Instantiate(randomCustomer, spawnPoint.position, Quaternion.identity);
        customer.transform.position = spawnPoint.position;
        customer.SetActive(true);


        yield return new WaitForSeconds(3);
        
        CloseDoor();
    }

    private System.Collections.IEnumerator WaitToCloseDoor()
    {
        yield return new WaitForSeconds(5);

        CloseDoor();
    }

    private void InitializeInactiveCustomers()
    {
        for (int i = 0; i < availableSeats.Length; i++)
        {
            GameObject randomCustomer = generateRandomCustomer();

            NPC_AI npcAI = randomCustomer.GetComponent<NPC_AI>();

            if (npcAI == null)
            {
                // Debug.LogError("NPCAI component not found on customer prefab");
                return;
            }

            GameObject chair = availableSeats[i];

            // Debug.Log("Chair: " + chair.name);

            npcAI.SetSitPoint(chair);

            GameObject customer = Instantiate(randomCustomer, spawnPoint.position, Quaternion.identity);
            customer.SetActive(false); // Set them as inactive
            inactiveCustomers.Add(customer); // Add to the inactive list

        }
    }

    private GameObject generateRandomCustomer()
    {
        return customerPrefabs[Random.Range(0, customerPrefabs.Length)];
    }

    private System.Collections.IEnumerator SpawnCustomers()
    {

        OpenDoor();
        // Loop to spawn customers until maxCustomers is reached
        for (int i = 0; i < availableSeats.Length; i++)
        {
            GameObject customer = inactiveCustomers[i];

            customer.transform.position = spawnPoint.position; // Reset position
            customer.SetActive(true); // Activate the customer
            activeCustomers.Add(customer); // Add to the list of active customers

            yield return new WaitForSeconds(spawnInterval); // Wait before next spawn
        }

        inactiveCustomers.Clear();
        CloseDoor();
    }

    // Method to remove a customer from the active list and deactivate them
    public void RemoveCustomer(GameObject customer)
    {
        if (activeCustomers.Contains(customer))
        {
            activeCustomers.Remove(customer);
        }
    }

    internal void OpenDoor()
    {
        GameObject door = doorAnimator.gameObject;
        if (door != null)
        {
            door.GetComponent<Animator>().Play("DoorOpen");
        }
    }

    internal void CloseDoor()
    {
        GameObject door = doorAnimator.gameObject;
        if (door != null)
        {
            door.GetComponent<Animator>().Play("DoorClose");
        }
    }
}
