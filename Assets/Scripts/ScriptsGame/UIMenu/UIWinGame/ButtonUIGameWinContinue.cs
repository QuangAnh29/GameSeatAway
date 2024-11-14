using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonUIGameWinContinue : BaseButton
{
    [SerializeField] private int _index;
    [SerializeField] private int _scoreWin;

    protected override void OnClick()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();

        //
        int score = PlayerPrefs.GetInt("Score", 0);
        int totalScore = score + _scoreWin;
        PlayerPrefs.SetInt("Score", totalScore);
        PlayerPrefs.Save();

        //
        SceneManager.LoadScene(_index);
    }
}
