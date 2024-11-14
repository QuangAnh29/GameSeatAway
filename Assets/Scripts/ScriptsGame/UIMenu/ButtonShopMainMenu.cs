using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShopMainMenu : BaseButton
{
    public GameObject paneShop;
    public GameObject paneLevel;
    public GameObject paneSetting;

    public GameObject backgroundShop;
    public GameObject backgroundLevel;
    public GameObject backgroundSetting;
    protected override void OnClick()
    {
        backgroundShop.SetActive(true);
        backgroundLevel.SetActive(false);
        backgroundSetting.SetActive(false);

        paneShop.SetActive(true);
        paneLevel.SetActive(false);
        paneSetting.SetActive(false);
        SoundManager.instance.PlaySFX("Click");
    }
}
