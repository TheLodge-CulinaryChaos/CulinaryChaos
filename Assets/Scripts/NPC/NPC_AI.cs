using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent(typeof(NavMeshAgent))]
public class NPC_AI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject sitPoint;
    public GameObject[] waypoints; // Waypoints for moving to the chair
    private int currWaypoint = -1; // For seating waypoints
    private bool isSitting = false;

    private NPCManager npcManager; // Reference to the NPC Manager
    public OrderSystem orderSystem;
    private bool hasOrdered = false;

    private Recipe order;

    private Coroutine waitingForOrder;
    private bool IsOrderCompleted = false;

    private Coroutine eatingToDestroy;

    //timer for frustration sound
    public AudioClip frustrationSound; // Add the frustration sound clip here
    public AudioClip[] greetingSounds;
    private AudioSource audioSource;
    private bool hasPlayedFrustrationSound = false; // Flag to track if sound has played

    private float waitTime = 0f;
    public float frustrationThreshold = 40f; // Time in seconds before NPC gets frustrated


    void Start()
    {
        // Grab references to the components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = 0.5f;
        navMeshAgent.autoBraking = true;

        audioSource = GetComponent<AudioSource>();

        PlayGreetSound();

        setNextWaypoint(); // Initialize the first waypoint
    }

    void Update()
    {

        if (IsOrderCompleted)
        {
            animator.SetBool("isEating", true);
            eatingToDestroy = StartCoroutine(EatAndDestroy());
        }

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (currWaypoint == waypoints.Length - 1)
            {
                StopMovement();

                GameObject chairObject = waypoints[currWaypoint].transform.parent.gameObject;
                transform.position = chairObject.transform.position;
                AlignToChair(chairObject);
                //PlayGreetSound();
                

                if (waitingForOrder == null) {
                    waitingForOrder = StartCoroutine(SitAndWaitForOrder());
                }

                //waitingForOrder = StartCoroutine(SitAndReturnToWaypoint());
                //waitingForOrder = StartCoroutine(SitAndWaitForOrder());
            }
            else
            {
                setNextWaypoint();
            }
        }
        // if (waitTime > frustrationThreshold * 2 && waitingForOrder != null)
        // {
        //     StopCoroutine(waitingForOrder);
        //     waitingForOrder = null;
        //     StartCoroutine(SitAndReturnToWaypoint());
        //     Debug.Log("NPC is frustrated and leaving the seat.");
        // }

        if (animator != null && !isSitting)
        {
            float normalizedSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            animator.SetFloat("vely", normalizedSpeed);
        }

        SitPointScript sitPointScript = sitPoint.GetComponent<SitPointScript>();
        GameObject orderObject = sitPointScript.orderObject;
        DiningOrderScript orderScript = orderObject.GetComponent<DiningOrderScript>();

        if (orderScript.IsOrderComplete() && !IsOrderCompleted)
        {
            StopCoroutine(waitingForOrder);
            IsOrderCompleted = true;
            orderSystem.RemoveOrder(order);
            order = null;
        }

    }

    private void PlayGreetSound() {
        if (audioSource != null && greetingSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, greetingSounds.Length); // Get a random index
            AudioClip selectedClip = greetingSounds[randomIndex];
            audioSource.clip = selectedClip;
            audioSource.Play();
            Debug.Log("NPC is saying hello with a random greeting!");
        }
    }


    private IEnumerator SitAndWaitForOrder()
    {
        // Generate an order for the NPC if not already ordered
        if (order == null)
        {
            order = GenerateOrder();
            
            waitTime = 0f;  // Reset the timer after the order is generated
            hasPlayedFrustrationSound = false; // Reset frustration sound flag
            Debug.Log("Order generated, starting wait timer.");

        }
        hasOrdered = true;

        // Start waiting only after the order is placed
        while (!IsOrderCompleted)
        {
            waitTime += Time.deltaTime;
            Debug.Log($"Waiting for order... Current waitTime: {waitTime} seconds");


            // Play frustration sound if wait time exceeds the threshold and sound hasn't been played
            if (waitTime >= frustrationThreshold && !hasPlayedFrustrationSound)
            {
                audioSource.clip = frustrationSound;
                audioSource.Play();
                hasPlayedFrustrationSound = true; // Prevents the sound from playing multiple times
                Debug.Log("Playing frustration sound due to wait time threshold exceeded.");
            }

            yield return null;
        }
        waitingForOrder = null;
        
    }


    public void SetSitPoint(GameObject sitPoint)
    {
        this.sitPoint = sitPoint;
        GameObject waypoint = sitPoint.transform.Find("sitPoint").gameObject;
        waypoints = new GameObject[] { waypoint };
    }

    private Recipe GenerateOrder()
    {
        if (hasOrdered) return null; // Prevent multiple orders


        // set order in the order object
        SitPointScript sitPointScript = sitPoint.GetComponent<SitPointScript>();

        // choosing randomly from the available recipes
        Recipe order = orderSystem.recipes[Random.Range(0, orderSystem.recipes.Count)];
        orderSystem.PlaceOrder(order, sitPointScript.tableNumber);
        // orderSystem.CreateOrderUI(order, sitPointScript.tableNumber);

        GameObject orderObject = sitPointScript.orderObject;
        DiningOrderScript orderScript = orderObject.GetComponent<DiningOrderScript>();

        orderScript.SetOrder(order);
        return order;
    }


    private void setNextWaypoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }

        if (currWaypoint < waypoints.Length - 1)
        {
            currWaypoint++;
            navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        else
        {
            isSitting = true;
            StopMovement();
        }
    }

    private void StopMovement()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        navMeshAgent.velocity = Vector3.zero;
        animator.SetFloat("vely", 0);
        animator.SetBool("isSitting", true);
    }

    private IEnumerator EatAndDestroy()
    {
        yield return new WaitForSeconds(3f);

        SitPointScript sitPointScript = sitPoint.GetComponent<SitPointScript>();
        GameObject orderObject = sitPointScript.orderObject;
        DiningOrderScript orderScript = orderObject.GetComponent<DiningOrderScript>();

        orderScript.ClearOrder();

        NPCManager npcManager = FindObjectOfType<NPCManager>();

        npcManager.AddMoreCustomer(sitPoint);
        npcManager.IncrementServedCustomers();

        Destroy(gameObject);

    }

    private IEnumerator SitAndReturnToWaypoint()
    {

        // Generate an order for the NPC
        if (order == null)
            order = GenerateOrder();

        hasOrdered = true;

        // Wait for 60 seconds (1 minute)
        yield return new WaitForSeconds(30f);

        // Stand up from sitting
        // isSitting = false; // Allow movement again
        // animator.SetBool("isSitting", false); // Reset sitting animation

        // navMeshAgent.isStopped = true;
        // navMeshAgent.velocity = Vector3.zero; // Clear any remaining velocity
        // animator.SetFloat("vely", 0); // Stop walking animation

        // Find the NPCManager and trigger GenerateCustomer to respawn a new customer
        NPCManager npcManager = FindObjectOfType<NPCManager>();
        if (npcManager != null)
        {
            npcManager.RemoveCustomer(gameObject);
            // npcManager.GenerateCustomer(); // Call the respawn method in the manager
        }

        // gameObject.SetActive(false); // Deactivate the NPC
    }



    private void AlignToChair(GameObject chairObject)
    {
        float chairRotationY = chairObject.transform.rotation.eulerAngles.y;

        if (Mathf.Abs(chairRotationY - 0f) < 10f)  // Close to 0 degrees
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Mathf.Abs(chairRotationY - 90f) < 10f)  // Close to 90 degrees
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Mathf.Abs(chairRotationY - 180f) < 10f)  // Close to 180 degrees
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Mathf.Abs(chairRotationY - 270f) < 10f)  // Close to 270 degrees
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }



}
