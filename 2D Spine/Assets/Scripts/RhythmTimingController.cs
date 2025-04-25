using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimingController : MonoBehaviour
{

    public float frameTime = 1f / 30f; // 30fps 기준
    public Animator aniWren;

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

        yield return WaitFrames(8);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC 울기 1");
        yield return WaitFrames(1);

        yield return WaitFrames(16);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC 울기 2");
        yield return WaitFrames(1);

        yield return WaitFrames(12); // 기준점보다 1프레임 전

        isInputTiming = true;
        yield return WaitFrames(4); // Fast 구간

        targetInputTime = Time.time;
        Debug.Log("정확한 입력 타이밍 (패턴1)");

        yield return WaitFrames(4); // Slow 구간
        isInputTiming = false;

        yield return WaitFrames(1); // 실제 플레이어 울기 연출
        isPlaying = false;
    }

    IEnumerator PlayPattern2Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(8);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC 울기 1");
        yield return WaitFrames(1);

        yield return WaitFrames(7);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC 울기 2");
        yield return WaitFrames(1);

        yield return WaitFrames(7);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC 울기 3");
        yield return WaitFrames(1);

        yield return WaitFrames(16); // 쉬는 타이밍

        yield return WaitFrames(12); // 기준점보다 1프레임 전

        isInputTiming = true;
        yield return WaitFrames(4); // Fast 구간

        targetInputTime = Time.time;
        Debug.Log("정확한 입력 타이밍 (패턴2)");

        yield return WaitFrames(4); // Slow 구간
        isInputTiming = false;

        yield return WaitFrames(1);
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
