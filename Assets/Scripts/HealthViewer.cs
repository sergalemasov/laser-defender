using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthViewer : MonoBehaviour
{
    private GameSession gameSession;
    private TextMeshProUGUI healthText = null;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        string newHealth = gameSession.GetHealth().ToString();

        healthText.SetText(newHealth);
    }
}
