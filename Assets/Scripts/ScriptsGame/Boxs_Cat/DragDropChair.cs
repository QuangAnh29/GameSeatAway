using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

public class DragDropChair : HCMonoBehaviour
{
    [SerializeField] private TypeColor _typeColor;

    private bool _isDragging;
    private GameObject _dragger;
    private SpringJoint _joint;

    [SerializeField] private float _dragSpeed = 10f;
    [SerializeField] private float _height = 12.5f;

    [SerializeField] private LayerMask _layerBase;
    [SerializeField] private List<HoldingBase> _holdingBaseUsing;
    [SerializeField] private QueueManagement _queueManagement;
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _positionRayCast;
    public List<ChairCtrl> listChairCtrls;
    [SerializeField] private Animator _animator;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;
    public bool IsDragging => _isDragging;

    [Title("Joint")]
    [SerializeField] private float _spring = 30f;
    [SerializeField] private float _damper = 5f;
    [SerializeField] private float _minDistance = 0f;
    [SerializeField] private float _maxDistance = 0.05f;

    private void Start()
    {
        DisplayHoldingBaseOfChair();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!_queueManagement.IsMoving && !_isDragging && _typeColor != TypeColor.White )
        {
            SoundManager.instance.PlaySFX("Drag");
            _initialPosition = transform.position;
            _isDragging = true;
            _queueManagement.IsDragDrop = true;
            _animator.SetBool("isPickedUp", true);
            _animator.SetBool("isPutDown", false);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            _dragger = new GameObject("Dragger");
            _dragger.transform.position = transform.position;
            _joint = gameObject.AddComponent<SpringJoint>();
            _joint.connectedBody = _dragger.AddComponent<Rigidbody>();
            _joint.connectedBody.useGravity = false;
            _joint.connectedBody.isKinematic = false;
            _joint.spring = _spring;
            _joint.damper = _damper;
            _joint.minDistance = _minDistance;
            _joint.maxDistance = _maxDistance;
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = Vector3.zero;
        }
    }

    private void OnMouseDrag()
    {
        if (_isDragging && !_queueManagement.IsMoving)
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _height);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            // Di chuyển dragger thay vì đối tượng chính
            _dragger.transform.position = Vector3.Lerp(_dragger.transform.position, objPosition, _dragSpeed * Time.deltaTime);
        }
    }

    private void OnMouseUp()
    {
        if (!_queueManagement.IsMoving && _isDragging)
        {
            SoundManager.instance.PlaySFX("Drop");
            _isDragging = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            _queueManagement.IsDragDrop = false;
            _animator.SetBool("isPickedUp", false);
            _animator.SetBool("isPutDown", true);
            Destroy(_dragger);
            Destroy(_joint);

            Ray ray = new Ray(_positionRayCast.transform.position, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerBase.value))
            {
                Vector3 planeCenter = hitInfo.transform.position;
                transform.position = planeCenter;
                DisplayHoldingBaseOfChair();
            }
            else
            {
                ReturnBase();
            }
            
        }
    }

    private void DisplayHoldingBaseOfChair()
    {
        for (int i = 0; i < listChairCtrls.Count; i++)
        {
            Ray ray1 = new Ray(listChairCtrls[i].transform.position, Vector3.down);
            if (Physics.Raycast(ray1, out RaycastHit hitInfo1, Mathf.Infinity, _layerBase.value))
            {
                if (_holdingBaseUsing[i] != null)
                {
                    _holdingBaseUsing[i].CanMove = true;
                }
                if (hitInfo1.collider.TryGetComponent(out HoldingBase newHoldingBase))
                {
                    _holdingBaseUsing[i] = newHoldingBase;
                    _holdingBaseUsing[i].CanMove = false;
                    listChairCtrls[i].HoldingBaseUsing = _holdingBaseUsing[i];
                }
                else
                {
                    ReturnBase();
                }
            }
        }
    }

    private void ReturnBase()
    {
        transform.position = _initialPosition;
    }
}
