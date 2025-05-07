using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public GameObject resultImageOB;
    public GameObject resultImageOB2;
    public GameObject resultText;

    public Image resultImage;
    public Image resultImage2;
    public Sprite[] resultSprites;
    public Sprite[] resultSprites2;

    void Start()
    {
        resultImageOB.gameObject.SetActive(false);
        resultImageOB2.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.Result, this.transform.position);
        StartCoroutine(ShowResultWithDelay());
    }

    IEnumerator ShowResultWithDelay()
    {
        yield return new WaitForSeconds(1f);
        resultImageOB.gameObject.SetActive(true);

        int score = ScoreManager.instance.score;

        if (score >= 72)
        {
            resultImage.sprite = resultSprites[0];
            resultImage2.sprite = resultSprites2[0];
        }
        else if (score >= 60)
        {
            resultImage.sprite = resultSprites[1];
            resultImage2.sprite = resultSprites2[0];
        }
        else if (score >= 45)
        {
            resultImage.sprite = resultSprites[2];
            resultImage2.sprite = resultSprites2[1];
        }
        else if (score >= 30)
        {
            resultImage.sprite = resultSprites[3];
            resultImage2.sprite = resultSprites2[2];
        }
        else
        {
            resultImage.sprite = resultSprites[4];
            resultImage2.sprite = resultSprites2[2];
        }
        yield return new WaitForSeconds(2.5f);
        resultImageOB2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        resultText.gameObject.SetActive(true);
    }
}
