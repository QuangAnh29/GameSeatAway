using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public Text scoreDisplay;
    private int _score;

    private void Update()
    {
        _score = PlayerPrefs.GetInt("Score", 0);
        scoreDisplay.text = _score.ToString();
    }
}
