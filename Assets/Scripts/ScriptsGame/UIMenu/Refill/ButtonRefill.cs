using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRefill : BaseButton
{
    public GameObject panelButtonBuyScore;
    public GameObject panelButtonRefillHeart;
    [SerializeField] int _scoreRefill;

    int _score, _heart, _scoreSubtraction;

    protected override void OnClick()
    {
        GetData();
        _scoreSubtraction = _score - _scoreRefill;
        if (_scoreSubtraction >= 0)
        {
            PlayerPrefs.SetInt("Score", _scoreSubtraction);
            PlayerPrefs.SetInt("Health", 5);
            PlayerPrefs.Save();

            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        else
        {
            panelButtonBuyScore.SetActive(true);
            panelButtonRefillHeart.SetActive(false);
        }
    }

    void GetData()
    {
        _score = PlayerPrefs.GetInt("Score", 0);
        _heart = PlayerPrefs.GetInt("Health", 5);
    }
}
