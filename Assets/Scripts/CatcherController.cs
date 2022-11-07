using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {
    [SerializeField] private NPCManager NPCManager;

    public GameObject thrower { get; set; }

    [SerializeField]
    private GameObject _explosion;

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < NPCManager.NPCs.Count; i++)
        {
            GameObject npc = NPCManager.NPCs[i];
            if (Vector3.Distance(transform.position, npc.transform.position) < 2)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                print(thrower + " caught " + npc.GetComponent<NPCController>().partijNaam);
                NPCManager.DespawnNPC(npc);
                thrower.GetComponent<PlayerController>().AddPoints(1);

                Destroy(gameObject);
            }
        }
    }
}
