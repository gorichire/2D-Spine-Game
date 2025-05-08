using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionDisplaySettings : MonoBehaviour
{

    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private readonly Resolution[] supportedResolutions = new Resolution[]
    {
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 1280, height = 720 }
    };

    private int selectedResolutionIndex = 0;

    void Start()
    {
        // �ػ� ��� UI�� �ֱ�
        resolutionDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();

        for (int i = 0; i < supportedResolutions.Length; i++)
        {
            Resolution res = supportedResolutions[i];
            string option = res.width + " x " + res.height;
            options.Add(option);

            // ���� �ػ󵵿� ��ġ�ϸ� ����
            if (Screen.width == res.width && Screen.height == res.height)
            {
                selectedResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = selectedResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // ��üȭ�� ���� �ݿ�
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    public void ApplyDisplaySettings()
    {
        Resolution res = supportedResolutions[resolutionDropdown.value];
        bool isFullscreen = fullscreenToggle.isOn;

        Screen.SetResolution(res.width, res.height, isFullscreen);
        Debug.Log(" ���� �õ� ��: {res.width}x{res.height}, fullscreen: {isFullscreen}");
    }

    private void Update()
    {
        
    }
}
