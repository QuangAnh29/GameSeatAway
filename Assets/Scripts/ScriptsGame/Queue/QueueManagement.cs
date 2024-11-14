using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class QueueManagement : HCMonoBehaviour
{
    [Title("")]
    [SerializeField]
    private GameController _gameController;

    public Queue<PlayerCtrl> characterQueue = new Queue<PlayerCtrl>();
    private bool _isMoving;
    private bool _isDragDrop;

    private float _sampleTimer = 0;

    private int _lastSuccessMovementCount = 0;

    public bool IsMoving => _isMoving;
    public bool IsDragDrop
    {
        get { return _isDragDrop; }
        set { _isDragDrop = value; }
    }

    private void OnEnable()
    {
        RegisterEvents();
    }

    private void OnDisable()
    {
        UnregisterEvents();
    }

    void RegisterEvents()
    {
        foreach (var player in _gameController.players)
        {
            player.GetComponent<PlayerMovement>().OnMovementStarted += HandlePlayerMovementStarted;
            player.GetComponent<PlayerMovement>().OnMovementFinished += HandlePlayerMovementFinished;
        }
    }

    void UnregisterEvents()
    {
        foreach (var player in _gameController.players)
        {
            if (player != null)
            {
                player.GetComponent<PlayerMovement>().OnMovementStarted -= HandlePlayerMovementStarted;
                player.GetComponent<PlayerMovement>().OnMovementFinished -= HandlePlayerMovementFinished;
            }
        }
    }

    private void HandlePlayerMovementStarted(PlayerMovement playerMovement)
    {
        _lastSuccessMovementCount += 1;
        _isMoving = true;
    }

    private void HandlePlayerMovementFinished(PlayerMovement playerMovement)
    {
        _lastSuccessMovementCount -= 1;
        if (_lastSuccessMovementCount == 0)
        {
            _isMoving = false;
        }

        else
        {
            TryMoveNextPlayer();
        }
    }

    private void FixedUpdate()
    {
        _sampleTimer -= Time.deltaTime;
        if (!_isMoving || !_isDragDrop)
        {
            ArrangeCharacters();
        }
        MoveRemainCatToStandPosition();
    }



    public void ArrangeCharacters()
    {
        characterQueue.Clear();
        foreach (var player in _gameController.players)
        {
            characterQueue.Enqueue(player);
        }

        TryMoveNextPlayer();
    }

    private void TryMoveNextPlayer()
    {
        if (characterQueue.Count == 0 || _isDragDrop)
            return;

        if (_sampleTimer >= 0)
            return;

        var nextPlayer = characterQueue.Dequeue();
        var player = nextPlayer.GetComponent<PlayerMovement>();
        if (player != null)
        {
            var moveSuccessfully = player.Move();
            if (moveSuccessfully)
            {
                player.MoveToStandPosition();
                MakeAllRemainCatMoveToStandPosition();
            }
            _sampleTimer = 0.3f;
        }
    }

    private void MakeAllRemainCatMoveToStandPosition()
    {
        var catList = _gameController.players;
        var catCount = catList.Count;
        var standList = _gameController.standPositions;

        for (var i = 0; i < catCount; i++)
        {
            catList[i].GetComponent<PlayerMovement>().SetStandPosition(standList[i].transform.position);
        }
    }

    private void MoveRemainCatToStandPosition()
    {
        var catList = _gameController.players;

        foreach (var cat in catList)
        {
            var mover = cat.GetComponent<PlayerMovement>();
            mover.MoveToStandPosition();
        }
    }
}
