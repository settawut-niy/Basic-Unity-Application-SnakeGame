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
    bool m_isGameMenuActive;
    public bool IsGameMenuActive
    {
        get { return m_isGameMenuActive; }
    }

    [Header("In-Game Score")]
    [SerializeField] RectTransform panel_InGameUI;
    [SerializeField] Text text_InGameCurrentScore;
    [SerializeField] Text text_InGameHighestScore;

    [Header("Instruction")]
    [SerializeField] Image panel_Instruction;

    [Header("Initail Game Pop Up")]
    [SerializeField] Image panel_InitailGamePopUp;

    public enum UIType
    {
        GameMenu,
        InGameUI,
        Instruction,
        InitailGamePopUp
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetActiveUI(UIType.Instruction, true);

        CheckAudioStateToogle();
    }

    void Update()
    {
        ShowOrHideGameMenu();

        ShowOrHideInstruction();
    }

    void ShowOrHideGameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel_GameMenu.gameObject.activeSelf && GameManager.instance.currentGameState == GameState.Playing)
            {
                GameManager.instance.StopPlay();
                AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);
            }
            else if (panel_GameMenu.gameObject.activeSelf && GameManager.instance.currentGameState == GameState.Stop)
            {
                GameManager.instance.InitialGame();
                AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);
            }

            m_isGameMenuActive = panel_GameMenu.gameObject.activeSelf;
        }
    }

    void ShowOrHideInstruction()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (!panel_Instruction.gameObject.activeSelf)
            {
                SetActiveUI(UIType.Instruction, true);
            }
            else
            {
                SetActiveUI(UIType.Instruction, false);
            }

            AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);
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

    public void SetActiveUI (UIType uiType, bool isSet)
    {
        switch (uiType)
        {
            case UIType.GameMenu:
                SetActiveGameMenu(isSet);
                break;
            case UIType.InGameUI:
                panel_InGameUI.gameObject.SetActive(isSet);
                break;
            case UIType.Instruction:
                panel_Instruction.gameObject.SetActive(isSet);
                break;
            case UIType.InitailGamePopUp:
                panel_InitailGamePopUp.gameObject.SetActive(isSet);
                break;
        }
    }

    void SetActiveGameMenu(bool isSet)
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

    public void SetActiveAudioToggle ()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);

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

    public void CloseButton()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);
        GameManager.instance.InitialGame();
        m_isGameMenuActive = panel_GameMenu.gameObject.activeSelf;
    }

    public void PlayAgainButton ()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);
        GameManager.instance.NewGame();
    }

    public void ExitButton ()
    {
        AudioManager.instance.PlaySFX(AudioManager.SFXType.iClick);
        Application.Quit();
    }
}
