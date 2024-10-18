using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject[] waypoints; // Waypoints for moving to the chair
    public GameObject[] exitWaypoints; // Exit waypoints to walk to after sitting
    private int currWaypoint = -1; // For seating waypoints
    private int exitWaypointIndex = 0; // For exit waypoints
    private bool isSitting = false;

    private Vector3 lastWaypointPosition; // Track the last waypoint position

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
            if (currWaypoint == waypoints.Length - 1)
            {
                // Save the last waypoint position before sitting down
                lastWaypointPosition = waypoints[currWaypoint].transform.position;

                CloseDoor(); // Close the door when reaching the last waypoint
                StopMovement();

                GameObject chairObject = waypoints[currWaypoint].transform.parent.gameObject;
                transform.position = chairObject.transform.position;

                // Correct the rotation logic
                if (Mathf.Abs(chairObject.transform.rotation.y) < 0.01f)
                {
                    // Rotate to 0 degrees if close to 0
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    // Rotate to 180 degrees otherwise
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

                // Start sitting for one minute
                StartCoroutine(SitAndReturnToWaypoint());
            }
            else
            {
                setNextWaypoint();
            }
        }

        // Update animator's "vely" parameter based on agent's velocity and speed
        if (animator != null && !isSitting)
        {
            float normalizedSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            animator.SetFloat("vely", normalizedSpeed);
        }
    }

    private void setNextWaypoint()
    {
        // Handle empty waypoints array
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints set!");
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
            isSitting = true; // Set isSitting to true here
            StopMovement();
        }
    }

    private void setExitWaypoint()
    {
        // Handle empty exit waypoints array
        if (exitWaypoints.Length == 0)
        {
            Debug.LogError("No exit waypoints");
            return;
        }
        // Check if we've reached the last exit waypoint
        if (exitWaypointIndex < exitWaypoints.Length)
        {
            // Update NavMeshAgent destination to the exit waypoint
            navMeshAgent.SetDestination(exitWaypoints[exitWaypointIndex].transform.position);
            exitWaypointIndex++; // Increment exit waypoint index for the next exit waypoint
        }
        navMeshAgent.isStopped = true; // Resume movement
    
    }

    private void StopMovement()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath(); // Clear the path
        navMeshAgent.velocity = Vector3.zero; // Clear any remaining velocity
        animator.SetFloat("vely", 0); // Set the walking speed to 0 to stop walking animation
        animator.SetBool("isSitting", true); // Trigger the sitting animation
    }

    private IEnumerator SitAndReturnToWaypoint()
    {
        // Wait for 60 seconds (1 minute)
        yield return new WaitForSeconds(10f);

        // Stand up from sitting
        isSitting = false; // Allow movement again
        animator.SetBool("isSitting", false); // Reset sitting animation

        navMeshAgent.isStopped = false; // Resume movement
         if (animator != null && !isSitting)
        {
            float normalizedSpeed = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
            animator.SetFloat("vely", normalizedSpeed);
        }
        setExitWaypoint();
    }

    private void OpenDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        if (door != null)
        {
            door.GetComponent<Animator>().SetTrigger("DoorOpen");
        }
    }

    private void CloseDoor()
    {
        GameObject door = GameObject.FindGameObjectWithTag("Door");
        if (door != null)
        {
            door.GetComponent<Animator>().SetBool("isOpen", true); // Ensure the door closes
        }
    }
}
