using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Make sure the background audio keeps playing during scene changes.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    /// <summary>
    /// Stop audio that's already playing.
    /// </summary>
    private void Start()
    {
        var other = FindObjectsOfType<AudioSource>();
        if (other.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
