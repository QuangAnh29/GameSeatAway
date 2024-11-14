using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLevelMainMenu : BaseButton
{
    public GameObject paneShop;
    public GameObject paneLevel;
    public GameObject paneSetting;

    public GameObject backgroundShop;
    public GameObject backgroundLevel;
    public GameObject backgroundSetting;
    protected override void OnClick()
    {
        backgroundShop.SetActive(false);
        backgroundLevel.SetActive(true);
        backgroundSetting.SetActive(false);

        paneShop.SetActive(false);
        paneLevel.SetActive(true);
        paneSetting.SetActive(false);
        SoundManager.instance.PlaySFX("Click");
    }
}
