using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitTimeOut : BaseButton
{
    public GameObject panelTimeOut;
    public GameObject panelGameFailed;
    protected override void OnClick()
    {
        Time.timeScale = 0f;
        panelTimeOut.SetActive(false);
        panelGameFailed.SetActive(true);
    }
}
