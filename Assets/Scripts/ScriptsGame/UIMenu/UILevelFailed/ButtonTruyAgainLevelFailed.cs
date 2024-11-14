using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTruyAgainLevelFailed : BaseButton
{
    protected override void OnClick()
    {
        int health = PlayerPrefs.GetInt("Health", 5);

        if (health > 0)
        {
            int numberhealth = health - 1;
            PlayerPrefs.SetInt("Health", numberhealth);
            PlayerPrefs.Save();

            ///////////

            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            SceneManager.LoadScene(currentLevel);
            Time.timeScale = 1.0f;
        }
    }
}
