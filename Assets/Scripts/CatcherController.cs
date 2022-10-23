using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {

    public GameObject thrower { get; set; }

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < NPCController.NPCs.Count; i++)
        {
            GameObject npc = NPCController.NPCs[i];
            if (Vector3.Distance(transform.position, npc.transform.position) < 1)
            {
                Destroy(npc);
                Destroy(gameObject);
                print(thrower + " caught " + npc.GetComponent<NPCController>().partijNaam);
            }
        }
    }
}
