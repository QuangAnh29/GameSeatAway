using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayButtonLevel : MonoBehaviour
{
    public Text buttonText;
    private int _currentLevel = 1;

    private void Start()
    {
        GetData();
        UpdateButtonText();
    }

    void GetData()
    {
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    void UpdateButtonText()
    {
        buttonText.text = "Level " + _currentLevel;
    }
}
