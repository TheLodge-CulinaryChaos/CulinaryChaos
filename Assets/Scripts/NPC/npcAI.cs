using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public GameObject[] waypoints;
    private int currWaypoint = -1;

    void Start()
    {
        // Grab references to the components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        // Call setNextWaypoint to initialize the first waypoint
        setNextWaypoint();
    }

    void Update()
    {
        // Check if the agent has reached the current waypoint
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            setNextWaypoint();
        }

        // Update animator's "vely" parameter based on agent's velocity and speed
        if (animator != null)
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
            // Stop the NavMeshAgent when the last waypoint is reached
            navMeshAgent.isStopped = true;
            navMeshAgent.autoBraking = true;

            // Optionally, you can add any additional behavior here, such as playing an animation or disabling the agent.
            Debug.Log("Reached the last waypoint, stopping the NPC.");
        }
    }
}
