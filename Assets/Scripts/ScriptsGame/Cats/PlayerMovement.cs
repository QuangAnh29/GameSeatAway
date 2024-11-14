using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;

public class PlayerMovement : HCMonoBehaviour
{
    private List<HoldingBase> _listHoldingBase;
    private int _indexBase = 0;
    private Tween _tween;

    [SerializeField]
    private HoldingBase _start;

    [SerializeField]
    private PlayerCtrl _playerCtrl;

    [Title("")]
    [SerializeField]
    private GameController _gameController;

    [Title("Move")]
    [SerializeField]
    private float _timeMove = 0.2f;

    [Title("Jump")]
    [SerializeField]
    private float _jumpPower = 1.0f;
    [SerializeField]
    private int _numJumps = 1;
    [SerializeField]
    private float _duration = 0.5f;

    private Vector3 _standPosition;
    private bool _hasToMoveToStandPosition;

    public event Action<PlayerMovement> OnMovementStarted;
    public event Action<PlayerMovement> OnMovementFinished;

    [SerializeField] private Animator _animator;
    public static List<ChairCtrl> occupiedChairs = new List<ChairCtrl>();

    private ChairCtrl _targetChair;

    [SerializeField] private float _timeMoveToStandPosition;

    private PlayerPathfinding _pathfinding;

    private void Awake()
    {
        _pathfinding = new PlayerPathfinding(_start, _playerCtrl, _gameController);
    }

    [Button]
    public bool Move()
    {
        var chairAtStart = _pathfinding.GetChairAtStart();

        if (chairAtStart != null)
        {
            if (chairAtStart.CanSit(_playerCtrl.GetColor()) && !chairAtStart.IsPlayerSit)
            {
                _targetChair = chairAtStart;
                StartMovement(_targetChair);
                chairAtStart.IsPlayerSit = true;
                JumpToChair(_targetChair);
                _animator.SetBool("isJump", true);
                return true;
            }
            else
            {
                return false;
            }
        }

        var shortestPath = _pathfinding.FindShortestPathToChairSameColor();
        if (shortestPath == null || shortestPath.Count == 0 || (shortestPath.Count == 1 && !_pathfinding.CanReachChairSameColor()))
        {
            return false;
        }

        _animator.SetBool("isMove", true);
        _targetChair = _pathfinding.CheckCanSit();
        if (_targetChair != null)
        {
            StartMovement(_targetChair);
        }
        MovePath(shortestPath);

        return true;
    }

    public void SetStandPosition(Vector3 standPosition)
    {
        _standPosition = standPosition;
        _hasToMoveToStandPosition = true;
    }

    public void MoveToStandPosition()
    {
        if (!_hasToMoveToStandPosition)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _standPosition, _timeMoveToStandPosition * Time.deltaTime);
        _animator.SetBool("isMove", true);
        if (Vector3.Distance(transform.position, _standPosition) <= 0.01f)
        {
            transform.position = _standPosition;
            _hasToMoveToStandPosition = false;
            _animator.SetBool("isMove", false);
        }
    }

    private void MovePath(List<HoldingBase> listPos)
    {
        _listHoldingBase = listPos;
        MoveToNode();
    }

    private void MoveToNode()
    {
        if (_indexBase >= _listHoldingBase.Count)
        {
            JumpToChair(_targetChair);
            _indexBase = 0;
            return;
        }

        _tween?.Kill();

        var nextPosition = _listHoldingBase[_indexBase].transform.position;
        var targetRotation = Quaternion.LookRotation(nextPosition - transform.position);

        _tween = transform.DOMove(nextPosition, _timeMove).SetEase(Ease.Linear).OnStart(() =>
        {
            transform.rotation = targetRotation;
        }).OnComplete(() =>
        {
            _indexBase += 1;
            MoveToNode();
        });
    }

    private void JumpToChair(ChairCtrl chair = null)
    {
        if (chair == null)
        {
            chair = _targetChair;
        }
        if (chair != null)
        {
            var chairTransform = chair.positionPlayerSit.transform;
            var chairPosition = chairTransform.position;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.SetParent(chairTransform);
            _animator.SetBool("isJump", true);
            SoundManager.instance.PlaySFX("Jump");

            transform.DOJump(chairPosition, _jumpPower, _numJumps, _duration).OnComplete(() =>
            {
                chair.IsPlayerSit = true;
                chair.IsHoppInAnimation = true;
                EndMovement();
                StartCoroutine(ResetIsPlayerSit(chair));
            });
        }
        else
        {
            EndMovement();
        }
    }

    private IEnumerator ResetIsPlayerSit(ChairCtrl chair)
    {
        yield return new WaitForSeconds(1f);
        chair.IsHoppInAnimation = false;
    }

    private void StartMovement(ChairCtrl targetChair)
    {
        _playerCtrl.HasMoving = true;
        OnMovementStarted?.Invoke(this);
        if (targetChair != null && !occupiedChairs.Contains(targetChair))
        {
            occupiedChairs.Add(targetChair);
        }
    }

    private void EndMovement()
    {
        _playerCtrl.HasMoving = false;
        OnMovementFinished?.Invoke(this);
    }
}
