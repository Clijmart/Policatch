using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public static List<GameObject> NPCs = new List<GameObject>();

    public string partijNaam { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        partijNaam = "VVD";
        NPCs.Add(gameObject);
    }

    void Update()
    {
        float str = Mathf.Min(Time.deltaTime, 1);

        Vector3 lookDir = transform.position - player.transform.position;

        //This makes it only move on the y axis.
        lookDir.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(-lookDir);

        transform.rotation = Quaternion.Lerp(transform.rotation,
             targetRotation, str);
    }

    void OnDestroy()
    {
        NPCs.Remove(gameObject);
    }
}
