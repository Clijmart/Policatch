using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] TextAsset partiesFile;

    public static List<Party> parties = new List<Party>();

    void Start()
    {
        Parties partiesInJson = JsonUtility.FromJson<Parties>(partiesFile.text);

        foreach (Party party in partiesInJson.parties)
        {
            Debug.Log("Found party: " + party.name);
            parties.Add(party);
        }
    }

    public static Party RandomParty()
    {
        return parties[Random.Range(0, parties.Count)];
    }
}
