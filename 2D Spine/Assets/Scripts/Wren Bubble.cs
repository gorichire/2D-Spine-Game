using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WrenBubble : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Sprite[] allSprites;
    public Animator aniBubble;
    public GameObject Bubble;

    bool bubblecheck = false;

    private int[][] storyLines = new int[][]
{
    new int[] {0, 1, 2, 3},
    new int[] {0,1,2,4 },
    new int[] {5,6,7,8 },
    new int[] {5,6,7,9 },
    new int[] {10,11,12,13 },
    new int[] {10,11,12,14 },
    new int[] {15,16,17,18 },
    new int[] {19,20,21,22 },

}; 
    private int currentStoryIndex = 0; 
    private int currentFrameIndex = 0;
    public void PlayNext()
    {
        if (!bubblecheck)
        {
            gameObject.SetActive(true);
            Bubble.SetActive(true);
            aniBubble.SetTrigger("open");
            bubblecheck = true;

            StartCoroutine(ShowSpriteAfterOpenAnimation()); 
        }
        else
        {
            AdvanceSpriteImmediately(); 
        }
    }

    private IEnumerator ShowSpriteAfterOpenAnimation()
    {
        spriteRenderer.sprite = allSprites[storyLines[currentStoryIndex][currentFrameIndex]];
        yield return new WaitForSeconds(0.2f); // 스프라이트 말풍선 애니메이션 싱크 맞추기
    }

    private void AdvanceSpriteImmediately()
    {
        currentFrameIndex++;

        if (currentFrameIndex + 1 >= storyLines[currentStoryIndex].Length)
        {
            StartCoroutine(CloseBubbleAfterDelay());
            return;
        }

        spriteRenderer.sprite = allSprites[storyLines[currentStoryIndex][currentFrameIndex]];
    }

    private IEnumerator CloseBubbleAfterDelay()
    {
        spriteRenderer.sprite = allSprites[storyLines[currentStoryIndex][storyLines[currentStoryIndex].Length - 1]];

        yield return new WaitForSeconds(1f);

        aniBubble.SetTrigger("close");
        bubblecheck = false;
        currentFrameIndex = 0;
        currentStoryIndex++;
        yield return new WaitForSeconds(0.2f);
        Bubble.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
}
