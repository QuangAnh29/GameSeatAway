using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ChairCtrl : HCMonoBehaviour
{
    [Title("")]
    [SerializeField]
    private TypeColor _typeColor;

    [Title("")]
    [SerializeField]
    private List<Transform> _listPositionMoveCanSit;

    [SerializeField]
    private LayerMask _layerBase;

    [Title("")]
    [SerializeField]
    private bool _isPlayerSit;
    private bool _isHopInAnimation;

    [SerializeField]
    private HoldingBase _holdingBaseUsing;

    public GameObject positionPlayerSit;

    [SerializeField] private Animator _animator;

    private void Update()
    {
        _animator.SetBool("isHopIn", _isHopInAnimation);
    }

    public HoldingBase HoldingBaseUsing
    {
        get { return _holdingBaseUsing; }
        set { _holdingBaseUsing = value; }
    }

    public bool IsPlayerSit
    {
        get { return _isPlayerSit; }

        set
        {
            _isPlayerSit = value;
        }
    }

    public bool IsHoppInAnimation
    {
        get { return _isHopInAnimation; }

        set
        {
            _isHopInAnimation = value;
        }
    }
    public bool CanSit(TypeColor typeColor)
    {
        return typeColor == _typeColor;
    }

    public bool CheckPositionCanMove()
    {
        foreach (Transform position in _listPositionMoveCanSit)
        {
            Ray ray = new Ray(position.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerBase))
            {
                HoldingBase holdingBase = hitInfo.collider.GetComponentInParent<HoldingBase>();
                if (holdingBase != null)
                {
                    if (holdingBase.CanMove)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //Trả về danh sách các holdingBase gần ghế mà có thể di chuyển được đến ghế đó.
    public List<HoldingBase> DisplayHoldingBaseCanMove()
    {
        List<HoldingBase> canMoveHoldingBase = new List<HoldingBase>();
        if (CheckPositionCanMove())
        {
            foreach (Transform position in _listPositionMoveCanSit)
            {
                Ray ray = new Ray(position.position, Vector3.down);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerBase))
                {
                    HoldingBase holdingBase = hitInfo.collider.GetComponentInParent<HoldingBase>();

                    if (holdingBase != null && holdingBase.CanMove)
                    {
                        canMoveHoldingBase.Add(holdingBase);
                    }
                }
            }
        }
        return canMoveHoldingBase;
    }
}
