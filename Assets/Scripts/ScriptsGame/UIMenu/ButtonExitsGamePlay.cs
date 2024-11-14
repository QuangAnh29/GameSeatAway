using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitsGamePlay : BaseButton
{
    public GameObject panelGamePlay;
    protected override void OnClick()
    {
        panelGamePlay.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.PlaySFX("Click");
    }
}
