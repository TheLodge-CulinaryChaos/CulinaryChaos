using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{
    // TODO: figure out how to associate order with npc
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject[] waypoints;
    private int currWaypoint = -1;
    private bool isSitting = false;

    public OrderSystem orderSystem;

    void Start()
    {
        // Grab references to the components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = 0.5f; // Increase stopping distance to prevent overshooting
        navMeshAgent.autoBraking = true;


        // Call setNextWaypoint to initialize the first waypoint
        setNextWaypoint();
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (currWaypoint == waypoints.Length - 1)
            {
                // NPC has reached last waypoint and sat down
                StopMovement();

                GameObject chairObject = waypoints[currWaypoint].transform.parent.gameObject;
                transform.position = chairObject.transform.position;
                // rotate X 90 degrees
                transform.rotation = Quaternion.Euler(0, 180, 0);

                // now it can place an order
                if (!isSitting)
                {
                    isSitting = true;
                    Debug.Log("NPC has sat down");
                    // choosing randomly from the available recipes
                    Recipe order = orderSystem.recipes[Random.Range(0, orderSystem.recipes.Count)];
                    Debug.Log(order);
                    orderSystem.PlaceOrder(order);
                    orderSystem.CreateOrderUI(order);
                }
            }
            else
            {
                setNextWaypoint();
            }
        }

        // Update animator's "vely" parameter based on agent's velocity and speed
        if (animator != null && !isSitting)
        {
            navMeshAgent.isStopped = false;
            float normalizedSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            animator.SetFloat("vely", normalizedSpeed);

        }
    }


    private void setNextWaypoint()
    {
        // Handle empty waypoints array
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints set for AI_Minion!");
            return;
        }

        // Check if we've reached the last waypoint
        if (currWaypoint < waypoints.Length - 1)
        {
            // Increment the current waypoint index
            currWaypoint++;

            // Update NavMeshAgent destination to the next waypoint
            navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        else
        {
            isSitting = true; //
            StopMovement();
        }
    }


    private void StopMovement()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath(); // Clear the path
        navMeshAgent.velocity = Vector3.zero; // Clear any remaining velocity
        animator.SetFloat("vely", 0); // Set the walking speed to 0 to stop walking animation
        animator.SetBool("isSitting", true); // Trigger the sitting animation
    }
}

