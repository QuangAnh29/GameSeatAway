using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class GameController : HCMonoBehaviour
{
    [Title("Lists HoldingBase")]
    public List<HoldingBase> holdingBases = new List<HoldingBase>();

    [Title("Lists ChairCtrls")]
    public List<ChairCtrl> chairCtrls = new List<ChairCtrl>();

    [Title("Lists Player")]
    public List<PlayerCtrl> players = new List<PlayerCtrl>();

    [Title("Lists ChairCtrls")]
    public List<GameObject> standPositions = new List<GameObject>();


    [Title("GameUIController")]
    [SerializeField]
    private GameUIController _gameUIController;

    private void Update()
    {
        WinGame();
    }

    void WinGame()
    {
        bool allPlayersSit = true;
        foreach (var chair in chairCtrls)
        {
            if (!chair.IsPlayerSit)
            {
                allPlayersSit = false;
                break;
            }
        }

        if (allPlayersSit)
        {
            _gameUIController.ShowWinUI();
        }
    }

}