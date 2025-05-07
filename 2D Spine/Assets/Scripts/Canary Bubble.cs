using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanaryBubble : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite[] ca_allSprites;
    public Animator ca_aniBubble;
    public GameObject ca_Bubble;
    public GameObject ca_Story;
    public bool isTutorial = true;

    public SpriteRenderer scratchRenderer;
    public Sprite scratchSprite;

    private int currentStoryIndex = 0;
    private int currentFrameIndex = 0;
    public int CurrentFrameIndex => currentFrameIndex;

    private int[][] storyLines = new int[][]
    {
        new int[] {0, 1, 2, 3},
        new int[] {0, 1, 2, 4},
        new int[] {5, 6, 7, 8},
        new int[] {5, 6, 7, 9},
        new int[] {10, 11, 12, 13},
        new int[] {10, 11, 12, 14},
        new int[] {15, 16, 17, 18},
        new int[] {19, 20, 21, 22},
    };

    public void JudgePerfectOrGood()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.playerTweetSound, this.transform.position);
        if (isTutorial) return;
        if (!ca_Bubble.activeSelf)
        {
            ca_Story.SetActive(true);
            ca_Bubble.SetActive(true);
            ca_aniBubble.SetTrigger("open");
            StartCoroutine(ShowSpriteAfterOpen());
        }
        else
        {
            spriteRenderer.sprite = ca_allSprites[storyLines[currentStoryIndex][currentFrameIndex]];
        }
    }

    public void JudgeMiss()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.playerScreechSound, this.transform.position);
        if (ca_Bubble.activeSelf)
        {
            ca_aniBubble.SetTrigger("close");
            StartCoroutine(DeactivateBubbleAfterDelay()); // 닫는 애니 끝나고 꺼야 함
        }

        StartCoroutine(ShowScratch());
    }

    private IEnumerator ShowSpriteAfterOpen()
    {
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = ca_allSprites[storyLines[currentStoryIndex][currentFrameIndex]];
        
    }

    private IEnumerator ShowScratch()
    {
        scratchRenderer.sprite = scratchSprite;
        scratchRenderer.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        scratchRenderer.gameObject.SetActive(false);
    }

    public void AdvanceSprite()
    {
        if (isTutorial) return;
        spriteRenderer.sprite = ca_allSprites[storyLines[currentStoryIndex][currentFrameIndex]];
        currentFrameIndex++;

        if (currentFrameIndex >= storyLines[currentStoryIndex].Length)
        {
            StartCoroutine(CloseBubbleAfterDelay());
        }
    }

    public void FrameIndexPlus()
    {
        if (isTutorial) return;
        currentFrameIndex++;
        if (currentFrameIndex >= storyLines[currentStoryIndex].Length)
        {
            StartCoroutine(CloseBubbleAfterDelay());
        }
    }

    private IEnumerator CloseBubbleAfterDelay()
    {
        spriteRenderer.sprite = ca_allSprites[storyLines[currentStoryIndex][storyLines[currentStoryIndex].Length - 1]];

        yield return new WaitForSeconds(1f); // 마지막 스프라이트 1초 보여주기

        if (ca_Bubble.activeSelf)
        {
            ca_aniBubble.SetTrigger("close");
        }
        yield return new WaitForSeconds(0.2f); // 닫는 애니 끝날 때까지 기다리기 (0.3초 정도)

        ca_Bubble.SetActive(false);
        ca_Story.SetActive(false);

        currentFrameIndex = 0;
        currentStoryIndex++;
        spriteRenderer.sprite = null;
    }

    private IEnumerator DeactivateBubbleAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);

        ca_Bubble.SetActive(false);
        ca_Story.SetActive(false);
    }

}