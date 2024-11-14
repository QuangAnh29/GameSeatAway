using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPauseGamePlay : BaseButton
{
    public GameObject panelPause;

    protected override void OnClick()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0f;
        SoundManager.instance.PlaySFX("Click");
    }
}
