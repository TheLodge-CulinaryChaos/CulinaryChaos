using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject[] waypoints;
    private int currWaypoint = -1;
    private bool isSitting = false;

    void Start()
    {
        // Grab references to the components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = 0.5f; // Increase stopping distance to prevent overshooting
        navMeshAgent.autoBraking = true;

        OpenDoor();

        // Call setNextWaypoint to initialize the first waypoint
        setNextWaypoint();
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            CloseDoor();
            if (currWaypoint == waypoints.Length - 1)
            {


                StopMovement();

                GameObject chairObject = waypoints[currWaypoint].transform.parent.gameObject;
                transform.position = chairObject.transform.position;
                // rotate X 90 degrees
                transform.rotation = Quaternion.Euler(0, 180, 0);
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

    private void OpenDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        door.GetComponent<Animator>().SetTrigger("DoorOpen");
    }

    private void CloseDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        door.GetComponent<Animator>().SetBool("isOpen", true);
    }
}

