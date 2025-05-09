using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image titleImage;
    public Image blackImage;


    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.SongSelectBGM, this.transform.position);
        StartCoroutine(FadeOutImage(titleImage, 6f));   
        StartCoroutine(FadeOutImage(blackImage, 7f));
    }

    IEnumerator FadeOutImage(Image image, float delay)
    {
        yield return new WaitForSeconds(delay);

        float duration = 1f;
        float elapsed = 0f;
        Color startColor = image.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            image.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        image.color = new Color(startColor.r, startColor.g, startColor.b, 0f); 
    }
}
