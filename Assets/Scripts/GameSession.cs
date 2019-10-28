using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int totalScore = 0;

    public void AddScore(int score)
    {
        totalScore += score;
    }

    public int GetScore()
    {
        return totalScore;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    void Awake()
    {
        SetupSingleton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetupSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
