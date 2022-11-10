using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private PartyManager PartyManager;

    /// <summary>
    /// Generate a new random task.
    /// </summary>
    /// <returns>A random task</returns>
    public Task GenerateTask()
    {
        Party party = PartyManager.RandomParty();
        return new Task(party, party.RandomDescription());
    }
}
