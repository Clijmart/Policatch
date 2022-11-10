using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float despawnTime = 2f;

    /// <summary>
    /// Despawn the explosion after a certain amount of time.
    /// </summary>
    void Start()
    {
        Destroy(gameObject, despawnTime);
    }
}
