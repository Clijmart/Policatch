using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private PartyManager PartyManager;
    [SerializeField] private GameObject playerObject;

    public static List<GameObject> NPCs = new List<GameObject>();

    /// <summary>
    /// Spawn NPCs at all spawnpoints without occupants.
    /// </summary>
    private void Start()
    {
        foreach (var (prefab, transforms) in SpawnPoint.spawnPoints)
        {
            foreach (SpawnPoint spawn in transforms)
            {
                if (spawn.GetOccupant() == null)
                {
                    SpawnNPC(playerObject, spawn);
                }
            }
        }
    }

    /// <summary>
    /// Spawn an NPC at a spawnpoint.
    /// </summary>
    /// <param name="player">The player that the NPC will follow</param>
    /// <param name="spawnPoint">The spawnpoint the NPC will spawn at</param>
    /// <returns>The instantiated NPC object</returns>
    public GameObject SpawnNPC(GameObject player, SpawnPoint spawnPoint)
    {
        // Place the object at a spawnpoint without it being in the round
        Vector3 position = spawnPoint.transform.position;
        position.y += npcPrefab.transform.lossyScale.y / 2;

        // Instantiate the object and assign it's attributes
        GameObject npc = Instantiate(npcPrefab, position, Quaternion.identity);
        npc.GetComponent<NPCController>().spawn = spawnPoint;
        npc.GetComponent<NPCController>().party = PartyManager.RandomParty().name;
        npc.GetComponent<NPCController>().player = player;
        
        NPCs.Add(npc);
        spawnPoint.SetOccupant(npc);

        return npc;
    }

    /// <summary>
    /// Despawn an NPC object.
    /// </summary>
    /// <param name="npc">The NPC to despawn</param>
    public void DespawnNPC(GameObject npc)
    {
        npc.GetComponent<NPCController>().Despawn();
    }
}
