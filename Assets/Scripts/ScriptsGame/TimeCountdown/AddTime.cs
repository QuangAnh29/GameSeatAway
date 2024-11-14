using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : BaseButton
{
    public GameObject panelGameTimeOut;
    public TimeCountdown timeCountdown;

    public int scoreContinueToPlay;
    public GameObject panelBuyScore;

    protected override void OnClick()
    {
        
        int score = PlayerPrefs.GetInt("Score", 0);
        if(score >= scoreContinueToPlay)
        {
            int totalScore = score - scoreContinueToPlay;
            PlayerPrefs.SetInt("Score", totalScore);
            PlayerPrefs.Save();

            panelGameTimeOut.SetActive(false);
            timeCountdown.timeValue = 10;
        }
        else
        {
            panelBuyScore.SetActive(true);
        }
    }
}
