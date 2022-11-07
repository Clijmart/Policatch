using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    private Party party;
    private string description;

    Task(Party party, string description)
    {
        this.party = party;
        this.description = description;
    }

    public static Task GenerateTask()
    {
        Party party = PartyManager.RandomParty();
        return new Task(party, party.RandomDescription());
    }

    public Party Party()
    {
        return party;
    }

    public string Name()
    {
        return party.name;
    }

    public string Description()
    {
        return description;
    }
}
