using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class PlayerCtrl : HCMonoBehaviour
{
    [Title("")]
    [SerializeField] private TypeColor _color;

    [SerializeField] private bool _hasMoving;

    public event Action<PlayerCtrl> OnHasMovingChanged;

    public bool HasMoving
    {
        get => _hasMoving;
        set
        {
            if (_hasMoving != value)
            {
                _hasMoving = value;
                OnHasMovingChanged?.Invoke(this);
            }
        }
    }

    public TypeColor GetColor()
    {
        return _color;
    }
}
