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
    public Animator tutorialani;
    public GameObject tutorialOne;
    public GameObject tutorialTwo;
    public GameObject tutorialMask;

    private int tutorialCount = 0;

    public InputJudge inputJudge;
    public bool isInputTiming { get; private set; } = false;
    public float targetInputTime { get; private set; } = 0f;

    //private bool isPlaying = false;

    void Start()
    {
        StartCoroutine(PlayBGM());
    }



    public void PlayPattern1()
    {
        if (canaryBubble.isTutorial)
        {
            tutorialOne.gameObject.SetActive(true);
            tutorialMask.gameObject.SetActive(true);
            tutorialani.SetTrigger("One");
        }
        StartCoroutine(PlayPattern1Coroutine());
    }

    public void PlayPattern2()
    {
        if (canaryBubble.isTutorial)
        {
            tutorialTwo.gameObject.SetActive(true);
            tutorialMask.gameObject.SetActive(true);
            tutorialani.SetTrigger("Two");
        }
        StartCoroutine(PlayPattern2Coroutine());
    }

    IEnumerator PlayBGM()
    {
        yield return WaitFrames(460);
        //AudioManager.instance.PlayerOneShot(AudioManager.instance.musicEvent, this.transform.position);
        AudioManager.instance.musicInstance.start();
    }
        IEnumerator PlayPattern1Coroutine()
    {
        //isPlaying = true;
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.pattern1, this.transform.position);
        aniWren.SetTrigger("sing");
        wrenBubble.PlayNext();

        yield return WaitFrames(30);
        aniWren.SetTrigger("sing");

        isInputTiming = true;

        yield return WaitFrames(26); // 나머지 Good 구간
        targetInputTime = Time.time;
        yield return WaitFrames(26); 

        inputJudge.ResetInput();
        isInputTiming = false;
        //isPlaying = false;
        canaryBubble.FrameIndexPlus();
        OnTutorialPatternPlayed();
    }

    IEnumerator PlayPattern2Coroutine()
    {
        //isPlaying = true;
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.pattern2, this.transform.position);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(12);
        aniWren.SetTrigger("sing");

        yield return WaitFrames(14);
        aniWren.SetTrigger("sing");
        wrenBubble.PlayNext();

        yield return WaitFrames(36); // 쉬는 타이밍

        isInputTiming = true;

        yield return WaitFrames(26);

        targetInputTime = Time.time;

        yield return WaitFrames(26);

        inputJudge.ResetInput();
        isInputTiming = false;
        //isPlaying = false;
        canaryBubble.FrameIndexPlus();
        OnTutorialPatternPlayed();
    }

    IEnumerator WaitFrames(int frameCount)
    {
        yield return new WaitForSeconds(frameCount * frameTime);
    }

    // Update is called once per frame

    private void CheckMarkerTrigger()
    {
        string currentMarker = (string)AudioManager.instance.timelineInfo.lastMarker;

        if (currentMarker == "A")
        {
            PlayPattern1();
            // 중복 실행 방지를 위해 마커 초기화 또는 플래그 설정
            AudioManager.instance.timelineInfo.lastMarker = new FMOD.StringWrapper();
        }
        else if (currentMarker == "B")
        {
            PlayPattern2();
            AudioManager.instance.timelineInfo.lastMarker = new FMOD.StringWrapper();
        }
    }

    void Update()
    {
        CheckMarkerTrigger();
    }

    public void OnTutorialPatternPlayed()
    {
        if (tutorialOne.activeSelf)
        { 
            tutorialOne.gameObject.SetActive(false); 
        }
        if (tutorialTwo.activeSelf)
        {
            tutorialTwo.gameObject.SetActive(false);
        }
        tutorialMask.gameObject.SetActive(false);

        tutorialCount++;

        if (tutorialCount >= 4)
        {
            canaryBubble.isTutorial = false;
            wrenBubble.isTutorial = false;
        }
    }

}
