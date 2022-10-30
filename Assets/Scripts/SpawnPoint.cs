using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static Dictionary<GameObject, List<SpawnPoint>> spawnPoints = new Dictionary<GameObject, List<SpawnPoint>>();

    [SerializeField] private GameObject spawnPrefab;

    private GameObject occupant;

    // Start is called before the first frame update
    void Start()
    {
        if (!spawnPoints.ContainsKey(spawnPrefab))
        {
            spawnPoints.Add(spawnPrefab, new List<SpawnPoint>());
        }

        spawnPoints[spawnPrefab].Add(this);
    }

    public void setOccupant(GameObject o)
    {
        occupant = o;
    }

    public GameObject getOccupant()
    {
        return occupant;
    }
}
