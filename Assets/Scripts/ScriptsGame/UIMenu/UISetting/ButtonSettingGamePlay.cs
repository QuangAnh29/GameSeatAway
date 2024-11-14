using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSettingGamePlay : BaseButton
{
    public GameObject pannelSetting;
    protected override void OnClick()
    {

        pannelSetting.SetActive(true);
        Time.timeScale = 0;
        SoundManager.instance.PlaySFX("Click");
    }
}
