using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {

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
        for (int i = 0; i < NPCController.NPCs.Count; i++)
        {
            GameObject npc = NPCController.NPCs[i];
            if (Vector3.Distance(transform.position, npc.transform.position) < 2)
            {
                Instantiate(_explosion, transform.position, Quaternion.identity);
                Destroy(npc);
                Destroy(gameObject);
                print(thrower + " caught " + npc.GetComponent<NPCController>().partijNaam);
            }
        }
    }
}
