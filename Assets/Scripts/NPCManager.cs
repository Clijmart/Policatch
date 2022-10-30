using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;

    public static List<GameObject> NPCs = new List<GameObject>();

    public GameObject SpawnNPC(GameObject player, SpawnPoint spawnPoint)
    {
        Vector3 position = spawnPoint.transform.position;
        position.y += npcPrefab.transform.lossyScale.y / 2;

        GameObject npc = Instantiate(npcPrefab, position, Quaternion.identity);
        npc.GetComponent<NPCController>().spawn = spawnPoint;
        npc.GetComponent<NPCController>().partijNaam = "VVD";
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
