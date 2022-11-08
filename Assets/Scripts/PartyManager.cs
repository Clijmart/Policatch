using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] TextAsset partiesFile;

    public static List<Party> parties = new List<Party>();

    public void LoadParties()
    {
        Parties partiesInJson = JsonUtility.FromJson<Parties>(partiesFile.text);

        foreach (Party party in partiesInJson.parties)
        {
            Debug.Log("Found party: " + party.name);
            parties.Add(party);
        }
    }

    public Party RandomParty()
    {
        if (parties.Count < 1) {
            LoadParties();
        }
        return parties[Random.Range(0, parties.Count)];
    }
}
