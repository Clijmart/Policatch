using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCManager NPCManager;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private TextMeshPro hologram;

    public GameObject player { get; set; }
    public string party { get; set; }
    public SpawnPoint spawn { get; set; }

    /// <summary>
    /// Update the NPC hologram and rotation.
    /// </summary>
    void Update()
    {
        // Update the hologram text
        hologram.text = party;

        // Make the npc look at the player
        float rotationStrength = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
        Vector3 lookDir = transform.position - player.transform.position;

        // This makes it only move on the y axis
        lookDir.y = 0;

        // Set a target rotation and rotate to it by the rotation strength
        Quaternion targetRotation = Quaternion.LookRotation(-lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation,
             targetRotation, rotationStrength);
    }

    /// <summary>
    /// Despawn the NPC.
    /// </summary>
    public void Despawn()
    {
        spawn.RemoveOccupant(player);
        NPCManager.NPCs.Remove(gameObject);
        Destroy(gameObject);
    }
}
