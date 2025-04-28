using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimingController : MonoBehaviour
{

    public float frameTime = 1f / 60f; // 60fps ����
    public Animator aniWren;
    public WrenBubble wrenBubble;
    public CanaryBubble canaryBubble;

    public InputJudge inputJudge;
    public bool isInputTiming { get; private set; } = false;
    public float targetInputTime { get; private set; } = 0f;

    private bool isPlaying = false;

    void Start()
    {
        
    }

    public void PlayPattern1()
    {
        if (!isPlaying)
            StartCoroutine(PlayPattern1Coroutine());
    }

    public void PlayPattern2()
    {
        if (!isPlaying)
            StartCoroutine(PlayPattern2Coroutine());
    }

    IEnumerator PlayPattern1Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(36);
        aniWren.SetTrigger("sing");
        wrenBubble.PlayNext();

        yield return WaitFrames(32);
        aniWren.SetTrigger("sing");

        isInputTiming = true;

        yield return WaitFrames(26); // ������ Good ����
        targetInputTime = Time.time;
        yield return WaitFrames(26); 

        inputJudge.ResetInput();
        isInputTiming = false;
        isPlaying = false;
        canaryBubble.AdvanceSprite();
    }

    IEnumerator PlayPattern2Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(12);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(12);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(14);
        aniWren.SetTrigger("sing");
        wrenBubble.PlayNext();

        yield return WaitFrames(26); // ���� Ÿ�̹�

        isInputTiming = true;

        yield return WaitFrames(26);

        targetInputTime = Time.time;

        yield return WaitFrames(26);

        inputJudge.ResetInput();
        isInputTiming = false;
        isPlaying = false;
        canaryBubble.AdvanceSprite();
    }

    IEnumerator WaitFrames(int frameCount)
    {
        yield return new WaitForSeconds(frameCount * frameTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
