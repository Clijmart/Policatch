using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI pointsText;

    void Update()
    {
        pointsText.text = player.GetComponent<PlayerController>().Points() + " points";
    }
}
