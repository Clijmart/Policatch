using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public static List<GameObject> NPCs = new List<GameObject>();

    public string partijNaam { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        partijNaam = "VVD";
        NPCs.Add(gameObject);
    }

    void OnDestroy()
    {
        NPCs.Remove(gameObject);
    }
}
