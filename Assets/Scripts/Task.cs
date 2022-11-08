using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    private Party party;
    private string description = "";

    public Task(Party party, string description)
    {
        this.party = party;
        this.description = description;
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
