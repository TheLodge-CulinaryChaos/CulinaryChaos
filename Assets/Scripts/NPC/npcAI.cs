using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class npcAI : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject[] waypoints; // Waypoints for moving to the chair
    private int currWaypoint = -1; // For seating waypoints
    private bool isSitting = false;

    private NPCManager npcManager; // Reference to the NPC Manager

    void Start()
    {
        // Grab references to the components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.stoppingDistance = 0.5f;
        navMeshAgent.autoBraking = true;



        OpenDoor();
        setNextWaypoint(); // Initialize the first waypoint
    }

    void Update()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (currWaypoint == waypoints.Length - 1)
            {
                CloseDoor();
                StopMovement();

                GameObject chairObject = waypoints[currWaypoint].transform.parent.gameObject;
                transform.position = chairObject.transform.position;
                AlignToChair(chairObject);

                StartCoroutine(SitAndReturnToWaypoint());
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

    private IEnumerator SitAndReturnToWaypoint()
{
    // Wait for 60 seconds (1 minute)
    yield return new WaitForSeconds(15f);

    // Stand up from sitting
    isSitting = false; // Allow movement again
    animator.SetBool("isSitting", false); // Reset sitting animation

    navMeshAgent.isStopped = true;
    navMeshAgent.velocity = Vector3.zero; // Clear any remaining velocity
    animator.SetFloat("vely", 0); // Stop walking animation

    // Find the NPCManager and trigger GenerateCustomer to respawn a new customer
    NPCManager npcManager = FindObjectOfType<NPCManager>();
    if (npcManager != null)
    {
        npcManager.RemoveCustomer(gameObject);
        npcManager.GenerateCustomer(); // Call the respawn method in the manager
    }

    gameObject.SetActive(false); // Deactivate the NPC
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
            door.GetComponent<Animator>().SetBool("isOpen", true);
        }
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
