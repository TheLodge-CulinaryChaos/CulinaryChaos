using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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

    private Order order;

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

    public TMP_Text tableNumberText;

    public GameObject bubblePanel;

    void Awake()
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
            StartCoroutine(EatAndDestroy());
        }

        if (
            !navMeshAgent.pathPending
            && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance
        )
        {
            if (currWaypoint == waypoints.Length - 1)
            {
                StopMovement();

                GameObject chairObject = waypoints[currWaypoint].transform.parent.gameObject;
                transform.position = chairObject.transform.position;
                AlignToChair(chairObject);

                if (waitingForOrder == null)
                {
                    waitingForOrder = StartCoroutine(SitAndWaitForOrder());
                }
            }
            else
            {
                setNextWaypoint();
            }
        }

        if (animator != null && !isSitting)
        {
            float normalizedSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            animator.SetFloat("vely", normalizedSpeed);
        }

        SitPointScript sitPointScript = sitPoint.GetComponent<SitPointScript>();
        GameObject orderObject = sitPointScript.orderObject;
        DiningOrderScript orderScript = orderObject.GetComponent<DiningOrderScript>();

        // set order status
        if (orderScript != null)
        {
            SetBubbleText(orderScript.getOrderStatus());
        }

        if (orderScript.IsOrderComplete() && !IsOrderCompleted)
        {
            StopCoroutine(waitingForOrder);
            IsOrderCompleted = true;
            orderSystem.RemoveOrder(order);
            order = null;
        }

        // order bubble status

        CheckAndPlayFrustrationSound();
    }

    private void CheckAndPlayFrustrationSound()
    {
        waitTime += Time.deltaTime;

        // Play frustration sound if wait time exceeds the threshold and sound hasn't been played
        if (waitTime >= frustrationThreshold && !hasPlayedFrustrationSound)
        {
            audioSource.clip = frustrationSound;
            audioSource.Play();
            hasPlayedFrustrationSound = true; // Prevents the sound from playing multiple times
        }
    }

    private void PlayGreetSound()
    {
        if (audioSource != null && greetingSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, greetingSounds.Length); // Get a random index
            AudioClip selectedClip = greetingSounds[randomIndex];
            audioSource.clip = selectedClip;
            audioSource.Play();
        }
    }

    public void SetSitPoint(GameObject sitPoint)
    {
        this.sitPoint = sitPoint;
        GameObject waypoint = sitPoint.transform.Find("sitPoint").gameObject;
        waypoints = new GameObject[] { waypoint };
    }

    private Order GenerateOrder()
    {
        if (hasOrdered)
            return null; // Prevent multiple orders

        // set order in the order object
        SitPointScript sitPointScript = sitPoint.GetComponent<SitPointScript>();

        // choosing randomly from the available recipes
        Recipe recipe = orderSystem.recipes[Random.Range(0, orderSystem.recipes.Count)];
        Order order = new Order(recipe, sitPointScript.tableNumber);
        orderSystem.PlaceOrder(order);

        GameObject orderObject = sitPointScript.orderObject;
        DiningOrderScript orderScript = orderObject.GetComponent<DiningOrderScript>();

        orderScript.SetOrder(order);
        tableNumberText.text = order.tableNumber.ToString();
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

        LeaveRestaurant();
    }

    private IEnumerator SitAndWaitForOrder()
    {
        // Generate an order for the NPC
        if (order == null)
            order = GenerateOrder();

        hasOrdered = true;

        // Wait for order time
        yield return new WaitForSeconds(order.recipe.time);

        // Find the NPCManager and trigger GenerateCustomer to respawn a new customer
        NPCManager npcManager = FindObjectOfType<NPCManager>();
        if (npcManager != null)
        {
            npcManager.AddMoreCustomer(sitPoint);
            LeaveRestaurant();
        }
    }

    private void LeaveRestaurant()
    {
        Destroy(gameObject);
    }

    internal void SetBubbleText(string text)
    {
        bool showPanel;
        if (bubblePanel == null || text.CompareTo("") == 0)
        {
            showPanel = false;
        }
        else
        {
            showPanel = true;
        }

        bubblePanel.SetActive(showPanel);
        bubblePanel.GetComponentInChildren<TMP_Text>().text = text;
    }

    private void AlignToChair(GameObject chairObject)
    {
        float chairRotationY = chairObject.transform.rotation.eulerAngles.y;

        if (Mathf.Abs(chairRotationY - 0f) < 10f) // Close to 0 degrees
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Mathf.Abs(chairRotationY - 90f) < 10f) // Close to 90 degrees
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (Mathf.Abs(chairRotationY - 180f) < 10f) // Close to 180 degrees
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Mathf.Abs(chairRotationY - 270f) < 10f) // Close to 270 degrees
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
    }
}
