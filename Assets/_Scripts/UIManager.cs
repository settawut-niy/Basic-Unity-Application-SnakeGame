using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Game Menu")]
    [SerializeField] Image panel_GameMenu;
    [SerializeField] Image snake_Normal;
    [SerializeField] Image snake_Confused;
    [SerializeField] Text text_GameMenuCurrentScore;
    [SerializeField] Text text_GameMenuHighestScore;
    [SerializeField] Button button_Close;
    [SerializeField] Toggle toggle_Audio;

    [Header("In-Game Score")]
    [SerializeField] RectTransform panel_InGameUI;
    [SerializeField] Text text_InGameCurrentScore;
    [SerializeField] Text text_InGameHighestScore;

    [Header("Instructor")]
    [SerializeField] Image panel_Instructor;

    [Header("Initail Game")]
    [SerializeField] Image panel_InitailGame;

    void Awake()
    {
        instance = this;

        SetActiveInstructor(true);

        CheckAudioStateToogle();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel_GameMenu.gameObject.activeSelf && GameManager.instance.currentGameState == GameState.Playing)
            {
                GameManager.instance.StopPlay();
                AudioManager.instance.PlayInteractSound("click");
            }
            else if (panel_GameMenu.gameObject.activeSelf && GameManager.instance.currentGameState == GameState.Stop)
            {
                GameManager.instance.InitialGame();
                AudioManager.instance.PlayInteractSound("click");
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!panel_Instructor.gameObject.activeSelf)
            {
                SetActiveInstructor(true);
            }
            else
            {
                SetActiveInstructor(false);
            }

            AudioManager.instance.PlayInteractSound("click");
        }
    }

    public void ScoreUpdate (bool isHighestScore, int score)
    {
        if (isHighestScore)
        {
            text_InGameHighestScore.text = score.ToString();
            text_GameMenuHighestScore.text = score.ToString();
        }
        else
        {
            text_InGameCurrentScore.text = score.ToString();
            text_GameMenuCurrentScore.text =  score.ToString();
        }
    }

    public void SetActiveGameMenu(bool isSet)
    {
        panel_GameMenu.gameObject.SetActive(isSet);

        if (GameManager.instance.currentGameState == GameState.GameOver)
        {
            snake_Confused.gameObject.SetActive(true);
            snake_Normal.gameObject.SetActive(false);

            button_Close.gameObject.SetActive(false);
        }
        else
        {
            snake_Confused.gameObject.SetActive(false);
            snake_Normal.gameObject.SetActive(true);

            button_Close.gameObject.SetActive(true);
        }
    }

    public void SetActiveInGameUI(bool isSet)
    {
        panel_InGameUI.gameObject.SetActive(isSet);
    }

    public void SetActiveInstructor(bool isSet)
    {
        panel_Instructor.gameObject.SetActive(isSet);
    }

    public void SetActiveInitialGame(bool isSet)
    {
        panel_InitailGame.gameObject.SetActive(isSet);
    }

    public void SetActiveAudioToggle ()
    {
        if (toggle_Audio.isOn)
        {
            AudioManager.instance.SetActiveAllAudio(true);
            PlayerPrefs.SetString("AudioState","isActive");
        }
        else
        {
            AudioManager.instance.SetActiveAllAudio(false);
            PlayerPrefs.SetString("AudioState", "!isActive");
        }
    }

    void CheckAudioStateToogle ()
    {
        if (PlayerPrefs.GetString("AudioState", "isActive") == "!isActive")
        {
            toggle_Audio.isOn = false;
        }
    }

    public void PlayAgainButton ()
    {
        GameManager.instance.NewGame();
    }

    public void ExitButton ()
    {
        Application.Quit();
    }
}
