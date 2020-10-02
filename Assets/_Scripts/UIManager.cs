using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Image panel_GameMenu;

    [SerializeField] Text text_InGameCurrentScore;
    [SerializeField] Text text_GameMenuCurrentScore;

    [SerializeField] Text text_InGameHighestScore;
    [SerializeField] Text text_GameMenuHighestScore;

    void Awake()
    {
        instance = this;
    }

    public void ScoreUpdate (bool isHighestScore, int score)
    {
        if (isHighestScore)
        {
            text_InGameHighestScore.text = "Highest Score: " + score.ToString();
            text_GameMenuHighestScore.text = "Highest Score: " + score.ToString();
        }
        else
        {
            text_InGameCurrentScore.text = "Score: " + score.ToString();
            text_GameMenuCurrentScore.text = "Score: " + score.ToString();
        }
    }

    public void SetActiveGameMenu(bool isSet)
    {
        panel_GameMenu.gameObject.SetActive(isSet);
    }

    public void PlayAgainButton ()
    {
        GameManager.instance.NewGame();
    }
}
