using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : HCMonoBehaviour
{
    public float timeValue;
    public Text timerText;

    [SerializeField] private GameUIController _gameUIController;

    private void Update()
    {
        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            CheckForLoss();
        }

        DisplayTime(timeValue);
    }



    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckForLoss()
    {
        _gameUIController.ShowLostUI();
    }
}
