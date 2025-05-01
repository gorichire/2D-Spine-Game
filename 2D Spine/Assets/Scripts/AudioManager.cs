using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
 
public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    public static AudioManager instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("¿¡·¯");
        }
        instance = this;

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
}
