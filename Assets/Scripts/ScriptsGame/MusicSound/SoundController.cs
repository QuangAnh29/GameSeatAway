using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    //public Slider musicSlider, sfxSlider;

    /*public void ToggleMusic()
    {
        SoundManager.instance.ToggleMusic();
    }*/

    public GameObject buttonSoundOn;
    public GameObject buttonSoundOff;

    private void Start()
    {
        UpdateSoundButton();
    }

    public void ToggleSFX()
    {
        SoundManager.instance.ToggleSFX();
        UpdateSoundButton();
    }


    private void UpdateSoundButton()
    {
        int mute = PlayerPrefs.GetInt("SFXMuted");
        if (mute == 1)
        {
            buttonSoundOn.gameObject.SetActive(false);
            buttonSoundOff.gameObject.SetActive(true);
        }
        else
        {
            buttonSoundOn.gameObject.SetActive(true);
            buttonSoundOff.gameObject.SetActive(false);
        }
    }

    /*public void MusicVolume()
    {
        SoundManager.instance.MusicVolume(musicSlider.value);
    }

    public void SFXVolume()
    {
        SoundManager.instance.SFXVolume(sfxSlider.value);
    }*/
}
