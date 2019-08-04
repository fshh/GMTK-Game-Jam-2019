using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseDeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject volumePanel;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highscoreText;
    [SerializeField] private float frequencyChangeTime = 0.5f;
    [SerializeField] private int frequencyChangeSteps = 100;

    private bool lower = true;
    private const float LOW_FREQ = 1000;
    private const float HIGH_FREQ = 22000;

    void Update()
    {
        if (Time.timeScale <= 0)
        {
            return;
        }

        if (PlayerController.IsDead() && !deathMenu.activeInHierarchy)
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
            lower = true;
            StartCoroutine("ChangeFrequencies");
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
                pauseMenu.SetActive(false);
                volumePanel.SetActive(false);
                Time.timeScale = 1f;
                lower = false;
                StartCoroutine("ChangeFrequencies");
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator ChangeFrequencies()
    {
        AudioLowPassFilter filter = GameObject.FindGameObjectWithTag("Audio Listener").GetComponent<AudioLowPassFilter>();
        filter.enabled = true;
        float fromFreq = (lower) ? HIGH_FREQ : LOW_FREQ;
        float toFreq = (lower) ? LOW_FREQ : HIGH_FREQ;
        filter.cutoffFrequency = fromFreq;
        for (float time = 0; time <= frequencyChangeTime; time += frequencyChangeTime / frequencyChangeSteps)
        {
            filter.cutoffFrequency = Mathf.Lerp(fromFreq, toFreq, time / frequencyChangeTime);
            yield return new WaitForSecondsRealtime(frequencyChangeTime / frequencyChangeSteps);
        }
        if (!lower)
        {
            filter.enabled = false;
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

    public void VolumePanel(bool open)
    {
        volumePanel.SetActive(open);
    }

    public void SetMaster(float volume)
    {
        mixer.SetFloat("Master", volume);
    }

    public void SetMusic(float volume)
    {
        mixer.SetFloat("Music", volume);
    }

    public void SetFX(float volume)
    {
        mixer.SetFloat("FX", volume);
    }
}
