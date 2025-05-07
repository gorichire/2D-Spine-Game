using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    [field: SerializeField]
    public EventReference musicEvent { get; private set; }
    public EventInstance musicInstance;

    public static AudioManager instance { get; private set; }

    public TimelineInfo timelineInfo;

    private GCHandle timelineHandle;

    private FMOD.Studio.EVENT_CALLBACK beatCallback;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdated;

    public delegate void MarkerListnerDelegate();
    public static event MarkerListnerDelegate makerUpdated;

    public static int lastBeat = 0;
    public static string lastMarkerString = null;

    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    private void Start()
    {
        if(instance != null)
        {
            timelineInfo = new TimelineInfo();
            beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
            //Unity (C#)에서는 메모리가 자동으로 움직임
            //GC(Garbage Collector)가 언제든지 timelineInfo를 옮길 수 있음.
            //하지만 FMOD은 native C 코드라서 주소가 정확히 여기에 있어야 함
            //그래서 GCHandleType.Pinned을 써서 timelineInfo를 절대 움직이지 않게 메모리에 "못 박는" 작업
            timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

            
        }
    }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("에러");

        }
        instance = this;
        musicInstance = RuntimeManager.CreateInstance(musicEvent);
        eventInstances = new List<EventInstance>();
    }

    public void PlayerOneShot(EventReference sound , Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }
    
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {
        foreach(EventInstance eventInstance in eventInstances) 
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void MusicOnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        timelineHandle.Free();
    }

    private void Update()
    {
        if(lastMarkerString != timelineInfo.lastMarker)
        {
            lastMarkerString = timelineInfo.lastMarker;
            if(makerUpdated != null)
            {
                makerUpdated();
            }
        }

        if(lastBeat != timelineInfo.currentBeat)
        {
            lastBeat = timelineInfo.currentBeat;
            if(beatUpdated != null)
            {
                beatUpdated();
            }
        }
    }

    void OnGUI()
    {
        GUILayout.Box($"Current Beat = {timelineInfo.currentBeat}. Last Marker = {(string)timelineInfo.lastMarker}");
    }


    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    //이 콜백은 FMOD에서 비트나 마커 등 특정 이벤트가 발생했을 때 Unity의 C# 함수가 안전하게 호출되도록 만드는 브리지 역할을 한다.
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        //getUserData()는 FMOD 인스턴스에 우리가 넘겨둔 C# 객체 주소를 다시 꺼내오기 위한 함수
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if(result != FMOD.RESULT.OK)
        {
            Debug.Log("Time Line Callback error: " + result);
        }
        else if(timelineInfoPtr != IntPtr.Zero)
        {
            //C 포인터로 받은 값을 → C# 객체로 복원하는 단계.
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            //콜백 타입에 따라 분기 처리
            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
        }
        return FMOD.RESULT.OK;

    }
}
