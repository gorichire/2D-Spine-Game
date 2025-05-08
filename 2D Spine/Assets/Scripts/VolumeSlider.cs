using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        BGM,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;
    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }
    private void Update()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.materVolume;
                break;
            case VolumeType.BGM:
                volumeSlider.value = AudioManager.instance.BGMVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
            default:
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.materVolume = volumeSlider.value;
                break;
            case VolumeType.BGM:
                AudioManager.instance.BGMVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                break;
            default:
                break;
        }
    }
}
