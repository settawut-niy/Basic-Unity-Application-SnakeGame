using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static ScoreBoard instance;

    int score = 0;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIManager.instance.ScoreUpdate(false, score);
        UIManager.instance.ScoreUpdate(true, PlayerPrefs.GetInt("HighestScore", 0));
    }

    public void AddScore(int scoreIncrease)
    {
        score += scoreIncrease;

        UIManager.instance.ScoreUpdate(false, score);

        if (score > PlayerPrefs.GetInt("HighestScore", 0))
        {
            PlayerPrefs.SetInt("HighestScore", score);
            UIManager.instance.ScoreUpdate(true, score);
        }
    }
}
