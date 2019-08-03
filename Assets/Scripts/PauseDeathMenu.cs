using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseDeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;

    void Update()
    {
        if (PlayerController.IsDead())
        {
            deathMenu.SetActive(true);
            GameObject scoreBoard = GameObject.FindGameObjectWithTag("Score Board");
            scoreText.text = scoreBoard.GetComponentInChildren<Text>().text;
            scoreBoard.GetComponent<ScoreBoard>().Score = PlayerPrefs.GetInt("Highscore");
            highscoreText.text = scoreBoard.GetComponentInChildren<Text>().text;
            scoreBoard.SetActive(false);
        }
        else if (Input.GetButtonDown("Pause"))
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            StartCoroutine("CheckForUnpause");
        }
    }

    private IEnumerator CheckForUnpause()
    {
        yield return null;
        while (true)
        {
            if (Input.GetButtonDown("Pause"))
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 1f;
                yield break;
            }
            yield return null;
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        // Make sure this is the correct scene
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
