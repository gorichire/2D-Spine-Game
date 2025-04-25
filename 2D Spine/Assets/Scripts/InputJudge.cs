using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJudge : MonoBehaviour
{
    public RhythmTimingController controller;
    public Animator aniCanary;

    // 30fps 기준 1프레임 = 약 0.033초
    public float perfectWindow = 8f / 30f;
    public float goodWindow = 15f / 30f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controller.PlayPattern1();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controller.PlayPattern2();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!controller.isInputTiming)
            {
                Debug.Log("Miss! (타이밍 아님)");
                return;
            }

            float diff = Mathf.Abs(Time.time - controller.targetInputTime);

            if (diff <= perfectWindow)
            {
                aniCanary.SetTrigger("sing");
                Debug.Log("Perfect!");
            }
            else if (diff <= goodWindow)
            {
                Debug.Log("Good!");
            }
            else
            {
                Debug.Log("Miss!");
            }
        }
    }
    void Start()
    {
        
    }

}
