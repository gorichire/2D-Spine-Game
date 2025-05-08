using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MainUI : MonoBehaviour 
{
    public RectTransform titleImage;
    public RectTransform birdImage;
    public RectTransform startButton;
    public RectTransform optionButton;
    public RectTransform exitButton;

    public GameObject optionPanel;

    public GameObject soundOptionOpen;
    public GameObject screenOptionOpen;

    void Start()
    {
        AudioManager.instance.PlayMainBGM();
        titleImage.anchoredPosition = new Vector2(0, 1000);
        titleImage.DOAnchorPosY(300, 2).SetEase(Ease.OutBack);

        birdImage.anchoredPosition = new Vector2(0, -1000);
        birdImage.DOAnchorPosY(-200, 2).SetEase(Ease.OutBack);

        startButton.anchoredPosition = new Vector2(1500, -100); 
        startButton.DOAnchorPosX(0, 2f).SetEase(Ease.OutExpo).SetDelay(2f);

        optionButton.anchoredPosition = new Vector2(1500, -300);
        optionButton.DOAnchorPosX(0, 2f).SetEase(Ease.OutExpo).SetDelay(2.5f);

        exitButton.anchoredPosition = new Vector2(1500, -500);
        exitButton.DOAnchorPosX(0, 2f).SetEase(Ease.OutExpo).SetDelay(3f);
    }

    public void OnStartButton()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.StartSound, this.transform.position);
        AudioManager.instance.StopMainBGM();
        OnDestroy();
        SceneManager.LoadScene("SelectScene");
    }

    public void OnOptionButton()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.OptionSound, this.transform.position);
        optionPanel.SetActive(true);
        soundOptionOpen.SetActive(true);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    public void OnExitOptionButton()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.OptionCloseSound, this.transform.position);
        soundOptionOpen.SetActive(false);
        screenOptionOpen.SetActive(false);
        optionPanel.SetActive(false);
    }

    public void OnSoundOptionButton()
    {
        screenOptionOpen.SetActive(false);
        soundOptionOpen.SetActive(true);
    }

    public void OnScreenOptionButton()
    {
        soundOptionOpen.SetActive(false);
        screenOptionOpen.SetActive(true);
    }

    void OnDestroy()
    {
        titleImage?.DOKill();
        birdImage?.DOKill();
        startButton?.DOKill();
        optionButton?.DOKill();
        exitButton?.DOKill();
    }
}
