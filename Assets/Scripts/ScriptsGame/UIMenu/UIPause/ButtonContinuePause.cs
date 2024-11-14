using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContinuePause : BaseButton
{
    public GameObject panelPauseGamePlay;
    protected override void OnClick()
    {
        panelPauseGamePlay.SetActive(false);
        Time.timeScale = 1;
    }
}
