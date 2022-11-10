using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hologram : MonoBehaviour
{
    /// <summary>
    /// Rotation the hologram so it's always looking at the camera.
    /// </summary>
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}