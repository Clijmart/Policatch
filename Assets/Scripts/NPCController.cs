using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCManager NPCManager;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private TextMeshPro partyNameText;

    public GameObject player { get; set; }
    public string party { get; set; }
    public SpawnPoint spawn { get; set; }

    void Update()
    {
        partyNameText.text = party;

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
        spawn.removeOccupant(player);
        NPCManager.NPCs.Remove(gameObject);
        Destroy(gameObject);
    }
}
