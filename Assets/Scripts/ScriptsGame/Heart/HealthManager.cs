using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private int _numberHealth;
    private int _maxHealth = 5;
    public Text numberHealthText;
    public Button addHealthButton;
    public Text recoveryTimeHealth;

    private float _recoveryTime = 900f;
    private float _recoveryTimer;

    private void Start()
    {
        _numberHealth = PlayerPrefs.GetInt("Health", 5);
        _recoveryTimer = PlayerPrefs.GetFloat("RecoveryTimer", _recoveryTime);

        UpdateHealthUI();
    }

    private void Update()
    {
        if(_numberHealth < 0)
        {
            _numberHealth = 0;
        }

        if (_numberHealth < _maxHealth)
        {
            _recoveryTimer -= Time.deltaTime;

            if (_recoveryTimer <= 0)
            {
                _numberHealth++;
                _recoveryTimer = _recoveryTime;
                SaveHealth();
            }

            DisplayRecoveryTime(_recoveryTimer);
        }

        UpdateHealthUI();
    }

    private void DisplayRecoveryTime(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0);
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        recoveryTimeHealth.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    private void SaveHealth()
    {
        PlayerPrefs.SetInt("Health", _numberHealth);
        PlayerPrefs.SetFloat("RecoveryTimer", _recoveryTimer);
        PlayerPrefs.Save();
    }


    private void UpdateHealthUI()
    {

        numberHealthText.text = _numberHealth.ToString();

        if (_numberHealth >= _maxHealth)
        {
            _numberHealth = _maxHealth;
            addHealthButton.interactable = false;
            recoveryTimeHealth.text = "MAX";
        }
        else
        {
            addHealthButton.interactable = true;
        }
    }
}
