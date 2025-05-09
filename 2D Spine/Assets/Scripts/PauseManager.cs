using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPaused = false;
    private Bus bgmBus;

    private bool escAllowed = false;

    void Start()
    {
        bgmBus = RuntimeManager.GetBus("bus:/BGM");
        StartCoroutine(EnableEscAfterDelay(7f));
    }

    void Update()
    {
        if (!escAllowed) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        bgmBus.setPaused(true); 
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        bgmBus.setPaused(false);
    }

    public void ExitGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        bgmBus.setPaused(false);
        AudioManager.instance.MusicOnDestroy();
        SceneManager.LoadScene("SelectScene");
    }

    private IEnumerator EnableEscAfterDelay(float delay)
    {
        escAllowed = false;
        yield return new WaitForSecondsRealtime(delay);
        escAllowed = true;
    }

}