using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimingController : MonoBehaviour
{

    public float frameTime = 1f / 60f; // 60fps 기준
    public Animator aniWren;
    public WrenBubble wrenBubble;
    public CanaryBubble canaryBubble;

    public InputJudge inputJudge;
    public bool isInputTiming { get; private set; } = false;
    public float targetInputTime { get; private set; } = 0f;

    private bool isPlaying = false;

    void Start()
    {
        StartCoroutine(PlayBGM());
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

    IEnumerator PlayBGM()
    {
        yield return WaitFrames(460);
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.MeetTweetBGM, this.transform.position);
    }
        IEnumerator PlayPattern1Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(10);
        aniWren.SetTrigger("sing");
        wrenBubble.PlayNext();

        yield return WaitFrames(32);
        aniWren.SetTrigger("sing");

        isInputTiming = true;

        yield return WaitFrames(26); // 나머지 Good 구간
        targetInputTime = Time.time;
        yield return WaitFrames(26); 

        inputJudge.ResetInput();
        isInputTiming = false;
        isPlaying = false;
        canaryBubble.FrameIndexPlus();
    }

    IEnumerator PlayPattern2Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(10);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(12);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(14);
        aniWren.SetTrigger("sing");
        wrenBubble.PlayNext();

        yield return WaitFrames(26); // 쉬는 타이밍

        isInputTiming = true;

        yield return WaitFrames(26);

        targetInputTime = Time.time;

        yield return WaitFrames(26);

        inputJudge.ResetInput();
        isInputTiming = false;
        isPlaying = false;
        canaryBubble.FrameIndexPlus();
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
