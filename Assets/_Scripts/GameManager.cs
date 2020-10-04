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
        UIManager.instance.SetActiveUI(UIManager.UIType.InitailGamePopUp, true);
        UIManager.instance.SetActiveUI(UIManager.UIType.InGameUI, true);
        UIManager.instance.SetActiveUI(UIManager.UIType.GameMenu, false);
    }

    public void StartPlay()
    {
        currentGameState = GameState.Playing;
        Time.timeScale = 1;
        UIManager.instance.SetActiveUI(UIManager.UIType.InitailGamePopUp, false);
    }

    public void StopPlay()
    {
        currentGameState = GameState.Stop;
        Time.timeScale = 0;
        UIManager.instance.SetActiveUI(UIManager.UIType.GameMenu, true);
        UIManager.instance.SetActiveUI(UIManager.UIType.InGameUI, false);
    }

    public void GameOver()
    {
        currentGameState = GameState.GameOver;
        Time.timeScale = 0f;
        UIManager.instance.SetActiveUI(UIManager.UIType.GameMenu, true);
        UIManager.instance.SetActiveUI(UIManager.UIType.InGameUI, false);
        AudioManager.instance.PlaySFX(AudioManager.SFXType.GameOver);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(0);
    }
}
