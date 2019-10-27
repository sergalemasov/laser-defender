using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;

    private GameSession gameSession = null;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        gameSession.ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadSceneWithTimeout("Game Over"));   
    }

    public IEnumerator LoadSceneWithTimeout(string scene)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }
}
