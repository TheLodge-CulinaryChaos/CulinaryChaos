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


    void Start()
    {
        // Grab references to the components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = 0.5f;
        navMeshAgent.autoBraking = true;
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

                waitingForOrder = StartCoroutine(SitAndReturnToWaypoint());
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

        if (orderScript.IsOrderComplete() && !IsOrderCompleted)
        {
            StopCoroutine(waitingForOrder);
            IsOrderCompleted = true;
            orderSystem.RemoveOrder(order);
            order = null;
        }

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
        if (Mathf.Abs(chairObject.transform.rotation.y) < 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


}
