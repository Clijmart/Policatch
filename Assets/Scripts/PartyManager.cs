using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] TextAsset partiesFile;

    public static List<Party> parties = new List<Party>();

    /// <summary>
    /// Read the party data from JSON file.
    /// </summary>
    public void LoadParties()
    {
        Parties partiesInJson = JsonUtility.FromJson<Parties>(partiesFile.text);

        foreach (Party party in partiesInJson.parties)
        {
            parties.Add(party);
        }
    }

    /// <summary>
    /// Selects a random party from the list.
    /// </summary>
    /// <returns>A randomly selected party</returns>
    public Party RandomParty()
    {
        if (parties.Count < 1) {
            LoadParties();
        }
        return parties[Random.Range(0, parties.Count)];
    }
}
