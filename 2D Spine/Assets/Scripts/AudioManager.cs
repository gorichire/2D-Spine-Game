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
            //Unity (C#)������ �޸𸮰� �ڵ����� ������
            //GC(Garbage Collector)�� �������� timelineInfo�� �ű� �� ����.
            //������ FMOD�� native C �ڵ�� �ּҰ� ��Ȯ�� ���⿡ �־�� ��
            //�׷��� GCHandleType.Pinned�� �Ἥ timelineInfo�� ���� �������� �ʰ� �޸𸮿� "�� �ڴ�" �۾�
            timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

            
        }
    }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("����");

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
    //�� �ݹ��� FMOD���� ��Ʈ�� ��Ŀ �� Ư�� �̺�Ʈ�� �߻����� �� Unity�� C# �Լ��� �����ϰ� ȣ��ǵ��� ����� �긮�� ������ �Ѵ�.
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        //getUserData()�� FMOD �ν��Ͻ��� �츮�� �Ѱܵ� C# ��ü �ּҸ� �ٽ� �������� ���� �Լ�
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if(result != FMOD.RESULT.OK)
        {
            Debug.Log("Time Line Callback error: " + result);
        }
        else if(timelineInfoPtr != IntPtr.Zero)
        {
            //C �����ͷ� ���� ���� �� C# ��ü�� �����ϴ� �ܰ�.
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            //�ݹ� Ÿ�Կ� ���� �б� ó��
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
