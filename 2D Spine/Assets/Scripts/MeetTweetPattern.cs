using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using static UnityEditor.Experimental.GraphView.GraphView;
public class MeetTweetPattern : MonoBehaviour
{

    public RhythmTimingController controller;

    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TriggerPattern1()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.pattern1, this.transform.position);
        controller.PlayPattern1();
    }

    public void TriggerPattern2()
    {
        AudioManager.instance.PlayerOneShot(FMODEvents.instance.pattern2, this.transform.position);
        controller.PlayPattern2();
    }

    IEnumerator WaitFrames(int frameCount)
    {
        yield return new WaitForSeconds(frameCount * 1f / 60f);
    }
}
