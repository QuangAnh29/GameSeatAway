using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public Text levelDisplay;

    private void Awake()
    {
        int level = PlayerPrefs.GetInt("CurrentLevel", 1);

        levelDisplay.text = "Level " + level.ToString();
    }
}
