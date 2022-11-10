using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherManager : MonoBehaviour
{
    [SerializeField] private GameObject catcherPrefab;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float verticalSpeed;

    /// <summary>
    /// Spawn a catcher at a given location.
    /// </summary>
    /// <param name="pos">The spawn position</param>
    /// <param name="rot">The spawn rotation</param>
    /// <param name="player">The player the catcher belongs to</param>
    /// <returns>The instantiated catcher object</returns>
    public GameObject SpawnCatcher(Vector3 pos, Quaternion rot, GameObject player)
    {
        GameObject catcher = Instantiate(catcherPrefab, pos, rot);
        catcher.GetComponent<CatcherController>().thrower = player;

        Rigidbody body = catcher.GetComponent<Rigidbody>();
        body.velocity = catcher.transform.forward * horizontalSpeed + catcher.transform.up * verticalSpeed;

        return catcher;
    }
}
