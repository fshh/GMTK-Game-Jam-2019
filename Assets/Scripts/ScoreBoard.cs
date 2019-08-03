using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private int digits = 10;

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
            StringBuilder scoreString = new StringBuilder(); 
            for (int i = 0; i < digits - score.ToString().Length; i++)
            {
                scoreString.Append(0);
            }
            if (digits - score.ToString().Length < 0)
            {
                // Holy shit you spent some fucking time playing this game
                scoreString.Append("WTF HOW");

            }
            else
            {
                scoreString.Append(score.ToString());
            }
            text.text = scoreString.ToString();
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
