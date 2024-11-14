using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : BaseButton
{
    private int _currentLevel;
    private int _numberHealth;
    public GameObject panelRefillHeart;
    protected override void OnClick()
    {

        GetData();

        if (_numberHealth > 0)
        {
            SceneManager.LoadScene(_currentLevel);
            Time.timeScale = 1;
        }
        else 
        { 
            panelRefillHeart.SetActive(true);
        }
    }

    void GetData()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        _numberHealth = PlayerPrefs.GetInt("Health", 5);
    }
}
