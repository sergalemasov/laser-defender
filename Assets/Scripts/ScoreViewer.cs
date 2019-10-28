using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreViewer : MonoBehaviour
{
    private GameSession gameSession;
    private TextMeshProUGUI scoreText = null;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        string newScore = gameSession.GetScore().ToString();

        scoreText.SetText(newScore);
    }
}
