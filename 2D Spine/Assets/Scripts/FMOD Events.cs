using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Tweet SFX")]

    [field: SerializeField] public EventReference playerTweetSound { get; private set; }

    [field: SerializeField] public EventReference playerScreechSound { get; private set; }

    [field: SerializeField] public EventReference pattern1 { get; private set; }
    [field: SerializeField] public EventReference pattern2 { get; private set; }

    [field: SerializeField] public EventReference Result { get; private set; }

    [field: SerializeField] public EventReference UIMouse { get; private set; }

    [field: SerializeField] public EventReference StartSound { get; private set; }
    [field: SerializeField] public EventReference OptionSound { get; private set; }
    [field: SerializeField] public EventReference OptionCloseSound { get; private set; }
    [field: SerializeField] public EventReference GameSelectSound { get; private set; }


    [field: Header("BGM")]

    //[field: SerializeField] public EventReference MeetTweetBGM { get; private set; }
    [field: SerializeField] public EventReference SongSelectBGM { get; private set; }

    [field: SerializeField] public EventReference MainBGM { get; private set; }


    public static FMODEvents instance { get; private set; }

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("����");
        }
        instance = this;
    }

    void Start()
    {
        if (Camera.main != null && Camera.main.GetComponent<StudioListener>() == null)
        {
            Camera.main.gameObject.AddComponent<FMODUnity.StudioListener>();
        }
    }


}
