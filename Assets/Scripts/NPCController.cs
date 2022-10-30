using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCManager NPCManager;
    [SerializeField] private float rotationSpeed;

    public GameObject player { get; set; }
    public string partijNaam { get; set; }
    public SpawnPoint spawn { get; set; }

    void Update()
    {
        float str = Mathf.Min(rotationSpeed * Time.deltaTime, 1);

        Vector3 lookDir = transform.position - player.transform.position;

        //This makes it only move on the y axis.
        lookDir.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(-lookDir);

        transform.rotation = Quaternion.Lerp(transform.rotation,
             targetRotation, str);
    }

    public void Despawn()
    {
        spawn.setOccupant(null);
        NPCManager.NPCs.Remove(gameObject);
        Destroy(gameObject);
    }
}
