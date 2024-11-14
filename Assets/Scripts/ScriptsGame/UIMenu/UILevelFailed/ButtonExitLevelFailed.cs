using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonExitLevelFailed : BaseButton
{
    [SerializeField] private int _indexMainMenu;
    protected override void OnClick()
    {
        int health = PlayerPrefs.GetInt("Health", 5);

        if (health >= 0) 
        {
            int numberhealth = health - 1; 
            PlayerPrefs.SetInt("Health", numberhealth);
            PlayerPrefs.Save();

            Time.timeScale = 1;  
            SceneManager.LoadScene(_indexMainMenu);
        }
    }
}

