using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Stop,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameState currentGameState;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InitialGame();
    }

    public void InitialGame()
    {
        currentGameState = GameState.Stop;
        Time.timeScale = 0;
        UIManager.instance.SetActiveGameMenu(false);
    }

    public void StartPlay()
    {
        currentGameState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        currentGameState = GameState.GameOver;
        Time.timeScale = 0f;
        UIManager.instance.SetActiveGameMenu(true);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
