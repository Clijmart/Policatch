using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {
    [SerializeField] private NPCManager NPCManager;

    public GameObject thrower { get; set; }

    [SerializeField] private GameObject correctExplosion;
    [SerializeField] private int correctPoints = 1;
    [SerializeField] private GameObject wrongExplosion;
    [SerializeField] private int wrongPoints = -1;
    [Space]
    [SerializeField] private float despawnTime = 5f;
    [SerializeField] private float catchDistance = 2.5f;

    /// <summary>
    /// Destroy the catcher after some time.
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, despawnTime);
    }

    /// <summary>
    /// Run catcher update to check for nearby NPCs.
    /// </summary>
    void Update()
    {
        for (int i = 0; i < NPCManager.NPCs.Count; i++)
        {
            GameObject npc = NPCManager.NPCs[i];
            if (Vector3.Distance(transform.position, npc.transform.position) < catchDistance)
            {
                Catch(npc);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Catch an NPC using a catcher.
    /// </summary>
    /// <param name="npc">The NPC to catch</param>
    void Catch(GameObject npc)
    {
        NPCController npcController = npc.GetComponent<NPCController>();
        PlayerController playerController = thrower.GetComponent<PlayerController>();
        if (npcController.party.Equals(playerController.CurrentTask().Name()))
        {
            Instantiate(correctExplosion, transform.position, Quaternion.identity);
            playerController.ChangePoints(correctPoints);
        }
        else
        {
            Instantiate(wrongExplosion, transform.position, Quaternion.identity);
            playerController.ChangePoints(wrongPoints);
        }

        playerController.RefreshTask();
        NPCManager.DespawnNPC(npc);
    }
}
