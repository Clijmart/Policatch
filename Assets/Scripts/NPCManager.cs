using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private PartyManager PartyManager;
    [SerializeField] private GameObject playerObject;

    public static List<GameObject> NPCs = new List<GameObject>();

    private void Start()
    {
        foreach (var (prefab, transforms) in SpawnPoint.spawnPoints)
        {
            foreach (SpawnPoint spawn in transforms)
            {
                if (spawn.getOccupant() == null)
                {
                    SpawnNPC(playerObject, spawn);
                }
            }
        }
    }

    public GameObject SpawnNPC(GameObject player, SpawnPoint spawnPoint)
    {
        Vector3 position = spawnPoint.transform.position;
        position.y += npcPrefab.transform.lossyScale.y / 2;

        GameObject npc = Instantiate(npcPrefab, position, Quaternion.identity);
        npc.GetComponent<NPCController>().spawn = spawnPoint;
        npc.GetComponent<NPCController>().party = PartyManager.RandomParty().name;
        npc.GetComponent<NPCController>().player = player;
        NPCs.Add(npc);

        spawnPoint.setOccupant(npc);

        return npc;
    }

    public void DespawnNPC(GameObject npc)
    {
        npc.GetComponent<NPCController>().Despawn();
    }
}
