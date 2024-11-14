using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _lostUI;

    public void ShowWinUI()
    {
        _winUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowLostUI()
    {
        _lostUI.SetActive(true);
    }
}
