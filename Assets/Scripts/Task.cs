public class Task
{
    private Party party;
    private string description = "";

    public Task(Party party, string description)
    {
        this.party = party;
        this.description = description;
    }

    /// <summary>
    /// Get the party in this task.
    /// </summary>
    /// <returns>The party in this task</returns>
    public Party Party()
    {
        return party;
    }

    /// <summary>
    /// Get the name of the party in this task.
    /// </summary>
    /// <returns>The name of the party in this task</returns>
    public string Name()
    {
        return party.name;
    }

    /// <summary>
    /// Get the description of the party in this task.
    /// </summary>
    /// <returns>The description of the party in this task</returns>
    public string Description()
    {
        return description;
    }
}
