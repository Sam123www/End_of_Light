using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options_UI : MonoBehaviour
{
    public Slider audioSlider, BGMSlider, SFXSlider;
    private void Update()
    {
        AudioManager.instance.audioMixer.SetFloat("Main", audioSlider.value == -20 ? -80 : audioSlider.value);
        AudioManager.instance.audioMixer.SetFloat("BGM", BGMSlider.value == -20 ? -80 : BGMSlider.value);
        AudioManager.instance.audioMixer.SetFloat("SFX", SFXSlider.value == -20 ? -80 : SFXSlider.value);
    }
    public void SetFullScreen(bool value)
    {
        AudioManager.PlayButtonClick();
        if (value)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }
    public void BackButton()
    {
        AudioManager.PlayButtonClick();
        gameObject.SetActive(false);
    }
}
