using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonExitPause : BaseButton
{
    private int _numberHeart;
    private int _exitHeartsLost;
    [SerializeField] private int _senceMenu;
    protected override void OnClick()
    {
        GetData();
        _exitHeartsLost = _numberHeart - 1;
        PlayerPrefs.SetInt("Health", _exitHeartsLost);
        PlayerPrefs.Save();
        Time.timeScale = 1;
        SceneManager.LoadScene(_senceMenu);
    }

    void GetData()
    {
        _numberHeart = PlayerPrefs.GetInt("Health", 5);
    }
}
