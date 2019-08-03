using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            text.text = score.ToString();
            if (PlayerPrefs.GetInt("Highscore", -1) < score)
            {
                PlayerPrefs.SetInt("Highscore", score);
            }
        }
    }

    private Text text;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
    }
}
