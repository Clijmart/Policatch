using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private PartyManager PartyManager;

    public Task GenerateTask()
    {
        Party party = PartyManager.RandomParty();
        return new Task(party, party.RandomDescription());
    }
}
