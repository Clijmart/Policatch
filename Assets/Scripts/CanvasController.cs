using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI taskText;

    /// <summary>
    /// Update all values on the canvas.
    /// </summary>
    void Update()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        pointsText.text = playerController.Points() + " points";
        taskText.text = playerController.CurrentTask().Description();
    }
}
