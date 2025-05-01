using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class InputJudge : MonoBehaviour
{

    public RhythmTimingController controller;
    public Animator aniCanary;
    public Animator aniWren;
    public CanaryBubble canaryBubble;

    private float gameStartTime;
    private bool alreadyInput = false; // �Է��ߴ��� üũ

    public float perfectWindow = 15f / 60f;
    public float goodWindow = 30f / 60f;

    public void ResetInput()
    {
        alreadyInput = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.instance.PlayerOneShot(FMODEvents.instance.pattern1, this.transform.position);
            controller.PlayPattern1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.instance.PlayerOneShot(FMODEvents.instance.pattern2, this.transform.position);
            controller.PlayPattern2();
        }
        if (Time.time - gameStartTime < 8f)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!controller.isInputTiming)
            {
                Debug.Log("Miss! (Ÿ�̹� �ƴ�)");
                aniCanary.SetTrigger("miss_sing");
                aniWren.SetTrigger("angry");
                canaryBubble.JudgeMiss();
                return;
            }

            if (alreadyInput)
            {
                Debug.Log("�̹� �Է����� (����)");
                aniCanary.SetTrigger("miss_sing");
                aniWren.SetTrigger("angry");
                canaryBubble.JudgeMiss();
                return;
            }

            alreadyInput = true; // �Է��ߴٰ� ǥ��

            float diff = Mathf.Abs(Time.time - controller.targetInputTime);
            Debug.Log(diff);

            if (diff <= perfectWindow)
            {
                aniCanary.SetTrigger("sing");
                Debug.Log("Perfect!");
                if (canaryBubble.CurrentFrameIndex == 3)
                {
                    aniWren.SetTrigger("giggle");
                }
                else
                {
                    aniWren.SetTrigger("happy");
                }
                canaryBubble.JudgePerfectOrGood();
            }
            else if (diff <= goodWindow)
            {
                aniCanary.SetTrigger("good_sing");
                Debug.Log("Good!");
                aniWren.SetTrigger("good");
                canaryBubble.JudgePerfectOrGood();
            }
            else
            {
                Debug.Log("Miss!");
                aniCanary.SetTrigger("miss_sing");
                aniWren.SetTrigger("angry");
                canaryBubble.JudgeMiss();
            }
        }
    }
    void Start()
    {
        gameStartTime = Time.time;
    }

}
