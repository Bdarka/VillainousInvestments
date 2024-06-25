using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class OptionsScript : MonoBehaviour
{
    public AudioMixer audioMixer;

    public GameObject GraphicsWindow;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;


    // Start is called before the first frame update
    void Start()
    {
        ResolutionSetup();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }


    #region Graphics

    public void OpenGraphicsWindow()
    {
        if (GraphicsWindow.activeSelf == false)
        {
            GraphicsWindow.SetActive(true);
        }
        else
        { 
            GraphicsWindow.SetActive(false); 
        }
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ResolutionSetup()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resolutionList = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            // string option = "width" + "x" + "height;
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionList.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionList);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    #endregion

    public void ExitButton()
    {
        Application.Quit();
    }

   
}
