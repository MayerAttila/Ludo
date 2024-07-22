using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Linq;

public class MainMenu : MonoBehaviour
{
    //public AudioMixer audioMixer;

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    List<string> options = new List<string>();
    public Toggle isFullScreanToggler;

    static public int rezIndex;
    static public bool isFullScreanVal = true;
    

    private void Start()
    {
        resolutions = Screen.resolutions;
        LoadResolutionOptions();

        for (int i = 0; i < resolutions.Length; i++) 
        {
            Debug.Log(resolutions[i]);
        }

        if (SceneChanger.rezIndex > -1) 
        {
            rezIndex = SceneChanger.rezIndex;
            isFullScreanVal = SceneChanger.isFullScrean;
            resolutionDropdown.value = rezIndex;
            SetResolution(rezIndex);
            isFullScreanToggler.isOn = isFullScreanVal;
            SetFullscrean(isFullScreanVal);


        }
        

        

        
    }

    void LoadResolutionOptions() 
    {
        int indexOfCurrentResolution = 0;
        resolutionDropdown.ClearOptions(); //valamiert hibat ir

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (true)//resolutions[i].width / resolutions[i].height > 1.75 && resolutions[i].width / resolutions[i].height < 1.8) 
            {
                string option = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString() + "  " + resolutions[i].refreshRate.ToString() + "Hz";

                /*
                if (resolutions[i].refreshRate == 120) 
                {
                    options.Add(option);
                }
                */

                options.Add(option);


            }

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) indexOfCurrentResolution = i;
        }

        resolutionDropdown.AddOptions(options); //valamiert hibat ir

        resolutionDropdown.value = indexOfCurrentResolution;
        resolutionDropdown.RefreshShownValue();
        SetResolution(indexOfCurrentResolution);
    }

    /*public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume", volume);
    }*/

    public void SetQuality(int index) 
    {
        QualitySettings.SetQualityLevel(index, false);
    }

    public void SetFullscrean(bool isFullscrean) 
    {
        Screen.fullScreen = isFullscrean;
        isFullScreanVal = isFullscrean;
    }

    public void SetResolution(int index) 
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        rezIndex = index;
    }


}
