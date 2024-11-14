using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSettingMainMenu : BaseButton
{
    public GameObject paneSetting;
    public GameObject paneLevel;
    public GameObject panelShop;

    public GameObject backgroundShop;
    public GameObject backgroundLevel;
    public GameObject backgroundSetting;
    protected override void OnClick()
    {
        backgroundShop.SetActive(false);
        backgroundLevel.SetActive(false);
        backgroundSetting.SetActive(true);

        paneSetting.SetActive(true);
        paneLevel.SetActive(false);
        panelShop.SetActive(false);
        SoundManager.instance.PlaySFX("Click");
    }
}
