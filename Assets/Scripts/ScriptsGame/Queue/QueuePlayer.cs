using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class QueuePlayer : HCMonoBehaviour
{
    [Title("")]
    [SerializeField] private GameController _gameCtroller;
    [SerializeField] private float _moveSpeed = 0.5f;

    private void OnEnable()
    {
        foreach (var player in _gameCtroller.players)
        {
            player.OnHasMovingChanged += HandlePlayerHasMovingChanged;
        }
    }

    private void OnDisable()
    {
        foreach (var player in _gameCtroller.players)
        {
            player.OnHasMovingChanged -= HandlePlayerHasMovingChanged;
        }
    }

    private void HandlePlayerHasMovingChanged(PlayerCtrl player)
    {
        if (player.HasMoving)
        {
            RemoveCharacter(player);
        }
    }

    private void RemoveCharacter(PlayerCtrl player)
    {
        if (_gameCtroller.players.Contains(player))
        {
            int index = _gameCtroller.players.IndexOf(player);
            _gameCtroller.players.RemoveAt(index);
        }
    }
}