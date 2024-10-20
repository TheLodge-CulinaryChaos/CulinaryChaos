using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallHandler : MonoBehaviour
{
    public Transform level1SpawnPoint;  // Reference to the Level 1 spawn point
    public float fallThreshold = -10f;  // Y-coordinate threshold to detect falling

    void Update()
    {
        // Check if the player has fallen below the threshold
        if (transform.position.y < fallThreshold)
        {
            TeleportToLevel1();  // Teleport the player back to the spawn point
        }
    }

    void TeleportToLevel1()
    {
        // Move the player to the spawn point's position
        transform.position = level1SpawnPoint.position;
    }
}