using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Start()
    {
        var other = FindObjectsOfType<AudioSource>();
        if (other.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
