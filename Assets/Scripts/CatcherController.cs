using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherController : MonoBehaviour {
    [SerializeField] private NPCManager NPCManager;

    public GameObject thrower { get; set; }

    [SerializeField] private GameObject correctExplosion;
    [SerializeField] private GameObject wrongExplosion;

    private void Start()
    {
        Destroy(gameObject, 5.0f);
    }

    void Update()
    {
        for (int i = 0; i < NPCManager.NPCs.Count; i++)
        {
            GameObject npc = NPCManager.NPCs[i];
            if (Vector3.Distance(transform.position, npc.transform.position) < 2)
            {
                NPCController npcController = npc.GetComponent<NPCController>();
                PlayerController playerController = thrower.GetComponent<PlayerController>();
                if (npcController.party.Equals(playerController.CurrentTask().Name()))
                {
                    Instantiate(correctExplosion, transform.position, Quaternion.identity);
                    print(thrower + " caught " + npcController.party);
                    playerController.AddPoints(1);
                    playerController.RefreshTask();
                } else
                {
                    Instantiate(wrongExplosion, transform.position, Quaternion.identity);
                    print("Wrong party!");
                    playerController.AddPoints(-1);
                }

                NPCManager.DespawnNPC(npc);
                Destroy(gameObject);
            }
        }
    }
}
