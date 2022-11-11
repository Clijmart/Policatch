using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static Dictionary<GameObject, List<SpawnPoint>> spawnPoints = new Dictionary<GameObject, List<SpawnPoint>>();

    [SerializeField] private NPCManager NPCManager;
    [SerializeField] private GameObject spawnPrefab;
    [Space]
    [SerializeField] private float respawnTime = 5f;

    private GameObject occupant;

    /// <summary>
    /// Add spawnpoint to a list.
    /// </summary>
    void Awake()
    {
        if (!spawnPoints.ContainsKey(spawnPrefab))
        {
            spawnPoints.Add(spawnPrefab, new List<SpawnPoint>());
        }

        spawnPoints[spawnPrefab].Add(this);
        GetComponent<Renderer>().enabled = false;
    }

    /// <summary>
    /// Set the spawnpoint's occupant.
    /// </summary>
    /// <param name="o">The new occupant of the spawnpoint</param>
    public void SetOccupant(GameObject o)
    {
        occupant = o;
    }

    /// <summary>
    /// Get the spawnpoint's occupant.
    /// </summary>
    /// <returns>The occupant of the spawnpoint</returns>
    public GameObject GetOccupant()
    {
        return occupant;
    }

    /// <summary>
    /// Remove and respawn the current occupant.
    /// </summary>
    /// <param name="causer">The object that caused the occupant to be removed</param>
    public void RemoveOccupant(GameObject causer)
    {
        occupant = null;
        StartCoroutine(Respawn(causer));
    }

    /// <summary>
    /// Respawn an NPC at the spawnpoint.
    /// </summary>
    /// <param name="causer">The object that caused the occupant to be respawned</param>
    /// <returns>Enumerator used identify this coroutine in memory</returns>
    public IEnumerator Respawn(GameObject causer)
    {
        yield return new WaitForSeconds(respawnTime);
        if (GetOccupant() == null)
        {
            NPCManager.SpawnNPC(causer, this);
        }
    }
}
