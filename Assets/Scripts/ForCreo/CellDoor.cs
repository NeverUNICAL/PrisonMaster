using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace ForCreo
{
    public class CellDoor : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _targetPosition;
        [SerializeField] private Vector3 _defaultPosition;

        private CellsConteiner _cellsConteiner;
        private bool _isOpened = false;

        public bool IsOpened => _isOpened;

        public event UnityAction Opened;
        public event UnityAction Closed;

        private void Awake()
        {
            _cellsConteiner = GetComponentInParent<CellsConteiner>();
        }

        private void OnEnable()
        {
            _cellsConteiner.DoorButton.Reached += OnReached;
            _cellsConteiner.DoorButton.Exit += OnExit;
        }

        private void OnDisable()
        {
            _cellsConteiner.DoorButton.Reached -= OnReached;
            _cellsConteiner.DoorButton.Exit -= OnExit;
        }

        private void OnReached()
        {
            if (_isOpened == false)
                Open();
            else
                Close();
        }

        private void OnExit()
        {
            Close();
        }

        public void Open()
        {
            Move(_targetPosition, _duration, Opened);
            _isOpened = true;
        }

        private void Close()
        {
            Move(_defaultPosition, _duration, Closed);
            _isOpened = false;
        }

        private void Move(Vector3 target, float duration, UnityAction action)
        {
            transform.DOLocalMove(target, duration).OnComplete(() => action?.Invoke());
        }
    }
}