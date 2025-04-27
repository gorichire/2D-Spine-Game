using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimingController : MonoBehaviour
{

    public float frameTime = 1f / 60f; // 60fps 기준
    public Animator aniWren;

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

        yield return WaitFrames(32);
        aniWren.SetTrigger("sing");

        isInputTiming = true;

        yield return WaitFrames(26); // 나머지 Good 구간
        targetInputTime = Time.time;
        yield return WaitFrames(26); 

        inputJudge.ResetInput();
        isInputTiming = false;
        isPlaying = false;
    }

    IEnumerator PlayPattern2Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(16);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(16);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(18);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(30); // 쉬는 타이밍

        isInputTiming = true;

        yield return WaitFrames(26);

        Debug.Log("asdasd)");
        targetInputTime = Time.time;

        yield return WaitFrames(26);

        inputJudge.ResetInput();
        isInputTiming = false;
        isPlaying = false;
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
